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
import { leadsService } from '../services/leadsService';
import type { LeadDto, CreateLeadRequest, LeadPriority, LeadSource } from '../types/lead';

const priorityLabels = {
  1: 'Baixa',
  2: 'Média',
  3: 'Alta',
  4: 'Muito Alta',
};

const priorityColors: Record<number, 'default' | 'primary' | 'warning' | 'error'> = {
  1: 'default',
  2: 'primary',
  3: 'warning',
  4: 'error',
};

const sourceLabels = {
  1: 'Google',
  2: 'Instagram',
  3: 'Facebook',
  4: 'LinkedIn',
  5: 'Indicação',
  6: 'Cold Call',
  7: 'Evento',
  8: 'Outro',
};

export function Leads() {
  const [leads, setLeads] = useState<LeadDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [dialogOpen, setDialogOpen] = useState(false);
  const [editingLead, setEditingLead] = useState<LeadDto | null>(null);
  const [formData, setFormData] = useState<CreateLeadRequest>({
    name: '',
    segment: '',
    city: '',
    problemDescription: '',
    priority: 2,
    source: 1,
    website: '',
    instagram: '',
    phone: '',
  });

  useEffect(() => {
    loadLeads();
  }, []);

  const loadLeads = async () => {
    try {
      const response = await leadsService.getAll();
      setLeads(response.data);
    } catch (error) {
      console.error('Erro ao carregar leads:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleOpenDialog = (lead?: LeadDto) => {
    if (lead) {
      setEditingLead(lead);
      setFormData({
        name: lead.name,
        segment: lead.segment,
        city: lead.city,
        problemDescription: lead.problemDescription,
        priority: lead.priority,
        source: lead.source,
        website: lead.website || '',
        instagram: lead.instagram || '',
        phone: lead.phone || '',
      });
    } else {
      setEditingLead(null);
      setFormData({
        name: '',
        segment: '',
        city: '',
        problemDescription: '',
        priority: 2,
        source: 1,
        website: '',
        instagram: '',
        phone: '',
      });
    }
    setDialogOpen(true);
  };

  const handleCloseDialog = () => {
    setDialogOpen(false);
    setEditingLead(null);
  };

  const handleSubmit = async () => {
    try {
      if (editingLead) {
        await leadsService.update(editingLead.id, formData);
      } else {
        await leadsService.create(formData);
      }
      handleCloseDialog();
      loadLeads();
    } catch (error) {
      console.error('Erro ao salvar lead:', error);
    }
  };

  const handleDelete = async (id: string) => {
    if (window.confirm('Deseja realmente excluir este lead?')) {
      try {
        await leadsService.delete(id);
        loadLeads();
      } catch (error) {
        console.error('Erro ao excluir lead:', error);
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
        <Typography variant="h4">Leads</Typography>
        <Button
          variant="contained"
          startIcon={<Add />}
          onClick={() => handleOpenDialog()}
        >
          Novo Lead
        </Button>
      </Box>

      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Nome</TableCell>
              <TableCell>Segmento</TableCell>
              <TableCell>Cidade</TableCell>
              <TableCell>Prioridade</TableCell>
              <TableCell>Origem</TableCell>
              <TableCell>Criado em</TableCell>
              <TableCell align="right">Ações</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {leads.map((lead) => (
              <TableRow key={lead.id}>
                <TableCell>{lead.name}</TableCell>
                <TableCell>{lead.segment}</TableCell>
                <TableCell>{lead.city}</TableCell>
                <TableCell>
                  <Chip
                    label={priorityLabels[lead.priority]}
                    color={priorityColors[lead.priority]}
                    size="small"
                  />
                </TableCell>
                <TableCell>{sourceLabels[lead.source]}</TableCell>
                <TableCell>
                  {new Date(lead.createdAt).toLocaleDateString('pt-BR')}
                </TableCell>
                <TableCell align="right">
                  <IconButton
                    size="small"
                    onClick={() => handleOpenDialog(lead)}
                  >
                    <Edit />
                  </IconButton>
                  <IconButton
                    size="small"
                    onClick={() => handleDelete(lead.id)}
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
          {editingLead ? 'Editar Lead' : 'Novo Lead'}
        </DialogTitle>
        <DialogContent>
          <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2, mt: 2 }}>
            <TextField
              label="Nome"
              fullWidth
              value={formData.name}
              onChange={(e) => setFormData({ ...formData, name: e.target.value })}
              required
            />
            <TextField
              label="Segmento"
              fullWidth
              value={formData.segment}
              onChange={(e) => setFormData({ ...formData, segment: e.target.value })}
              required
            />
            <TextField
              label="Cidade"
              fullWidth
              value={formData.city}
              onChange={(e) => setFormData({ ...formData, city: e.target.value })}
              required
            />
            <TextField
              label="Descrição do Problema"
              fullWidth
              multiline
              rows={3}
              value={formData.problemDescription}
              onChange={(e) =>
                setFormData({ ...formData, problemDescription: e.target.value })
              }
              required
            />
            <TextField
              label="Prioridade"
              select
              fullWidth
              value={formData.priority}
              onChange={(e) =>
                setFormData({ ...formData, priority: Number(e.target.value) as LeadPriority })
              }
            >
              {Object.entries(priorityLabels).map(([value, label]) => (
                <MenuItem key={value} value={Number(value)}>
                  {label}
                </MenuItem>
              ))}
            </TextField>
            <TextField
              label="Origem"
              select
              fullWidth
              value={formData.source}
              onChange={(e) =>
                setFormData({ ...formData, source: Number(e.target.value) as LeadSource })
              }
            >
              {Object.entries(sourceLabels).map(([value, label]) => (
                <MenuItem key={value} value={Number(value)}>
                  {label}
                </MenuItem>
              ))}
            </TextField>
            <TextField
              label="Website"
              fullWidth
              value={formData.website}
              onChange={(e) => setFormData({ ...formData, website: e.target.value })}
            />
            <TextField
              label="Instagram"
              fullWidth
              value={formData.instagram}
              onChange={(e) => setFormData({ ...formData, instagram: e.target.value })}
            />
            <TextField
              label="Telefone"
              fullWidth
              value={formData.phone}
              onChange={(e) => setFormData({ ...formData, phone: e.target.value })}
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
