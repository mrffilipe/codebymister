import { useEffect, useState } from 'react';
import {
  Box,
  Button,
  Typography,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  MenuItem,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  IconButton,
  Chip,
  CircularProgress,
} from '@mui/material';
import { Add, Edit, Delete } from '@mui/icons-material';
import { outreachService } from '../services/outreachService';
import { leadsService } from '../services/leadsService';
import type { Outreach, CreateOutreachRequest } from '../types/outreach';
import { OutreachChannel, ResponseStatus } from '../types/outreach';
import type { LeadDto } from '../types/lead';

const channelLabels: Record<OutreachChannel, string> = {
  [OutreachChannel.WhatsApp]: 'WhatsApp',
  [OutreachChannel.Instagram]: 'Instagram',
  [OutreachChannel.Email]: 'Email',
  [OutreachChannel.Phone]: 'Telefone',
  [OutreachChannel.LinkedIn]: 'LinkedIn',
  [OutreachChannel.Facebook]: 'Facebook',
  [OutreachChannel.InPerson]: 'Presencial',
};

const statusLabels: Record<ResponseStatus, string> = {
  [ResponseStatus.NoResponse]: 'Sem Resposta',
  [ResponseStatus.Positive]: 'Positiva',
  [ResponseStatus.Neutral]: 'Neutra',
  [ResponseStatus.Negative]: 'Negativa',
  [ResponseStatus.NotInterested]: 'Não Interessado',
};

const statusColors: Record<ResponseStatus, 'default' | 'success' | 'error' | 'warning' | 'info'> = {
  [ResponseStatus.NoResponse]: 'default',
  [ResponseStatus.Positive]: 'success',
  [ResponseStatus.Neutral]: 'info',
  [ResponseStatus.Negative]: 'error',
  [ResponseStatus.NotInterested]: 'warning',
};

export function Outreaches() {
  const [outreaches, setOutreaches] = useState<Outreach[]>([]);
  const [leads, setLeads] = useState<LeadDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [dialogOpen, setDialogOpen] = useState(false);
  const [editingOutreach, setEditingOutreach] = useState<Outreach | null>(null);
  const [formData, setFormData] = useState<CreateOutreachRequest>({
    leadId: '',
    channel: OutreachChannel.WhatsApp,
    message: '',
    notes: '',
  });

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      const [outreachesRes, leadsRes] = await Promise.all([
        outreachService.getAll(),
        leadsService.getAll(),
      ]);
      setOutreaches(outreachesRes);
      setLeads(leadsRes.data);
    } catch (error) {
      console.error('Erro ao carregar dados:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleOpenDialog = (outreach?: Outreach) => {
    if (outreach) {
      setEditingOutreach(outreach);
      setFormData({
        leadId: outreach.leadId,
        channel: outreach.channel,
        message: outreach.message,
        notes: outreach.notes || '',
      });
    } else {
      setEditingOutreach(null);
      setFormData({
        leadId: '',
        channel: OutreachChannel.WhatsApp,
        message: '',
        notes: '',
      });
    }
    setDialogOpen(true);
  };

  const handleCloseDialog = () => {
    setDialogOpen(false);
    setEditingOutreach(null);
  };

  const handleSubmit = async () => {
    try {
      if (editingOutreach) {
        await outreachService.update(editingOutreach.id, {
          responseAt: undefined,
          responseStatus: editingOutreach.responseStatus,
          followUpSent: editingOutreach.followUpSent,
          notes: formData.notes,
        });
      } else {
        await outreachService.create(formData);
      }
      handleCloseDialog();
      loadData();
    } catch (error) {
      console.error('Erro ao salvar outreach:', error);
    }
  };

  const handleDelete = async (id: string) => {
    if (window.confirm('Deseja realmente excluir este outreach?')) {
      try {
        await outreachService.delete(id);
        loadData();
      } catch (error) {
        console.error('Erro ao excluir outreach:', error);
      }
    }
  };

  if (loading) {
    return (
      <Box display="flex" justifyContent="center" alignItems="center" minHeight="400px">
        <CircularProgress />
      </Box>
    );
  }

  return (
    <Box>
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={3}>
        <Typography variant="h4">Outreach</Typography>
        <Button
          variant="contained"
          startIcon={<Add />}
          onClick={() => handleOpenDialog()}
        >
          Novo Outreach
        </Button>
      </Box>

      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Lead</TableCell>
              <TableCell>Canal</TableCell>
              <TableCell>Enviado em</TableCell>
              <TableCell>Respondeu</TableCell>
              <TableCell>Status</TableCell>
              <TableCell align="right">Ações</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {outreaches.map((outreach) => (
              <TableRow key={outreach.id}>
                <TableCell>{outreach.leadName}</TableCell>
                <TableCell>{channelLabels[outreach.channel]}</TableCell>
                <TableCell>
                  {new Date(outreach.sentAt).toLocaleDateString('pt-BR')}
                </TableCell>
                <TableCell>
                  <Chip
                    label={outreach.responded ? 'Sim' : 'Não'}
                    color={outreach.responded ? 'success' : 'default'}
                    size="small"
                  />
                </TableCell>
                <TableCell>
                  <Chip
                    label={statusLabels[outreach.responseStatus]}
                    color={statusColors[outreach.responseStatus]}
                    size="small"
                  />
                </TableCell>
                <TableCell align="right">
                  <IconButton
                    size="small"
                    onClick={() => handleOpenDialog(outreach)}
                  >
                    <Edit />
                  </IconButton>
                  <IconButton
                    size="small"
                    onClick={() => handleDelete(outreach.id)}
                  >
                    <Delete />
                  </IconButton>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>

      <Dialog open={dialogOpen} onClose={handleCloseDialog} maxWidth="md" fullWidth>
        <DialogTitle>
          {editingOutreach ? 'Editar Outreach' : 'Novo Outreach'}
        </DialogTitle>
        <DialogContent>
          <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2, mt: 2 }}>
            <TextField
              label="Lead"
              select
              fullWidth
              value={formData.leadId}
              onChange={(e) => setFormData({ ...formData, leadId: e.target.value })}
              required
              disabled={!!editingOutreach}
            >
              {leads.map((lead) => (
                <MenuItem key={lead.id} value={lead.id}>
                  {lead.name}
                </MenuItem>
              ))}
            </TextField>
            <TextField
              label="Canal"
              select
              fullWidth
              value={formData.channel}
              onChange={(e) => setFormData({ ...formData, channel: Number(e.target.value) as OutreachChannel })}
              required
              disabled={!!editingOutreach}
            >
              {Object.entries(channelLabels).map(([value, label]) => (
                <MenuItem key={value} value={value}>
                  {label}
                </MenuItem>
              ))}
            </TextField>
            <TextField
              label="Mensagem"
              fullWidth
              multiline
              rows={4}
              value={formData.message}
              onChange={(e) => setFormData({ ...formData, message: e.target.value })}
              required
              disabled={!!editingOutreach}
            />
            <TextField
              label="Notas"
              fullWidth
              multiline
              rows={2}
              value={formData.notes}
              onChange={(e) => setFormData({ ...formData, notes: e.target.value })}
            />
          </Box>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCloseDialog}>Cancelar</Button>
          <Button onClick={handleSubmit} variant="contained">
            Salvar
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
}
