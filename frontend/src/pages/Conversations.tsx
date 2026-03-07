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
import { conversationService } from '../services/conversationService';
import { leadsService } from '../services/leadsService';
import type { Conversation, CreateConversationRequest } from '../types/conversation';
import { InterestLevel, Timing, ConversationStatus } from '../types/conversation';
import type { LeadDto } from '../types/lead';

const interestLabels: Record<InterestLevel, string> = {
  [InterestLevel.Low]: 'Baixo',
  [InterestLevel.Medium]: 'Médio',
  [InterestLevel.High]: 'Alto',
  [InterestLevel.VeryHigh]: 'Muito Alto',
};

const timingLabels: Record<Timing, string> = {
  [Timing.Immediate]: 'Imediato',
  [Timing.ThisWeek]: 'Esta Semana',
  [Timing.ThisMonth]: 'Este Mês',
  [Timing.NextMonth]: 'Próximo Mês',
  [Timing.ThreeMonths]: '3 Meses',
  [Timing.Undefined]: 'Indefinido',
};

const statusLabels: Record<ConversationStatus, string> = {
  [ConversationStatus.Active]: 'Ativa',
  [ConversationStatus.WaitingResponse]: 'Aguardando Resposta',
  [ConversationStatus.ProposalSent]: 'Proposta Enviada',
  [ConversationStatus.Closed]: 'Fechada',
  [ConversationStatus.Lost]: 'Perdida',
};

const statusColors: Record<ConversationStatus, 'success' | 'warning' | 'default' | 'info' | 'error'> = {
  [ConversationStatus.Active]: 'success',
  [ConversationStatus.WaitingResponse]: 'warning',
  [ConversationStatus.ProposalSent]: 'info',
  [ConversationStatus.Closed]: 'default',
  [ConversationStatus.Lost]: 'error',
};

export function Conversations() {
  const [conversations, setConversations] = useState<Conversation[]>([]);
  const [leads, setLeads] = useState<LeadDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [dialogOpen, setDialogOpen] = useState(false);
  const [editingConversation, setEditingConversation] = useState<Conversation | null>(null);
  const [formData, setFormData] = useState<CreateConversationRequest>({
    leadId: '',
    interestLevel: InterestLevel.Medium,
    timing: Timing.Undefined,
    notes: '',
    nextStep: '',
  });

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      const [conversationsRes, leadsRes] = await Promise.all([
        conversationService.getAll(),
        leadsService.getAll(),
      ]);
      setConversations(conversationsRes);
      setLeads(leadsRes.data);
    } catch (error) {
      console.error('Erro ao carregar dados:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleOpenDialog = (conversation?: Conversation) => {
    if (conversation) {
      setEditingConversation(conversation);
      setFormData({
        leadId: conversation.leadId,
        interestLevel: conversation.interestLevel,
        timing: conversation.timing,
        notes: conversation.notes,
        nextStep: conversation.nextStep || '',
      });
    } else {
      setEditingConversation(null);
      setFormData({
        leadId: '',
        interestLevel: InterestLevel.Medium,
        timing: Timing.Undefined,
        notes: '',
        nextStep: '',
      });
    }
    setDialogOpen(true);
  };

  const handleCloseDialog = () => {
    setDialogOpen(false);
    setEditingConversation(null);
  };

  const handleSubmit = async () => {
    try {
      if (editingConversation) {
        await conversationService.update(editingConversation.id, {
          ...formData,
          status: editingConversation.status,
        });
      } else {
        await conversationService.create(formData);
      }
      handleCloseDialog();
      loadData();
    } catch (error) {
      console.error('Erro ao salvar conversa:', error);
    }
  };

  const handleDelete = async (id: string) => {
    if (window.confirm('Deseja realmente excluir esta conversa?')) {
      try {
        await conversationService.delete(id);
        loadData();
      } catch (error) {
        console.error('Erro ao excluir conversa:', error);
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
        <Typography variant="h4">Conversas</Typography>
        <Button
          variant="contained"
          startIcon={<Add />}
          onClick={() => handleOpenDialog()}
        >
          Nova Conversa
        </Button>
      </Box>

      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Lead</TableCell>
              <TableCell>Interesse</TableCell>
              <TableCell>Timing</TableCell>
              <TableCell>Status</TableCell>
              <TableCell>Criada em</TableCell>
              <TableCell align="right">Ações</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {conversations.map((conversation) => (
              <TableRow key={conversation.id}>
                <TableCell>{conversation.leadName}</TableCell>
                <TableCell>{interestLabels[conversation.interestLevel]}</TableCell>
                <TableCell>{timingLabels[conversation.timing]}</TableCell>
                <TableCell>
                  <Chip
                    label={statusLabels[conversation.status]}
                    color={statusColors[conversation.status]}
                    size="small"
                  />
                </TableCell>
                <TableCell>
                  {new Date(conversation.createdAt).toLocaleDateString('pt-BR')}
                </TableCell>
                <TableCell align="right">
                  <IconButton
                    size="small"
                    onClick={() => handleOpenDialog(conversation)}
                  >
                    <Edit />
                  </IconButton>
                  <IconButton
                    size="small"
                    onClick={() => handleDelete(conversation.id)}
                  >
                    <Delete />
                  </IconButton>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>

      <Dialog open={dialogOpen} onClose={handleCloseDialog} maxWidth="xs" fullWidth>
        <DialogTitle>
          {editingConversation ? 'Editar Conversa' : 'Nova Conversa'}
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
              disabled={!!editingConversation}
            >
              {leads.map((lead) => (
                <MenuItem key={lead.id} value={lead.id}>
                  {lead.name}
                </MenuItem>
              ))}
            </TextField>
            <TextField
              label="Nível de Interesse"
              select
              fullWidth
              value={formData.interestLevel}
              onChange={(e) => setFormData({ ...formData, interestLevel: Number(e.target.value) as InterestLevel })}
              required
            >
              {Object.entries(interestLabels).map(([value, label]) => (
                <MenuItem key={value} value={value}>
                  {label}
                </MenuItem>
              ))}
            </TextField>
            <TextField
              label="Timing"
              select
              fullWidth
              value={formData.timing}
              onChange={(e) => setFormData({ ...formData, timing: Number(e.target.value) as Timing })}
              required
            >
              {Object.entries(timingLabels).map(([value, label]) => (
                <MenuItem key={value} value={value}>
                  {label}
                </MenuItem>
              ))}
            </TextField>
            <TextField
              label="Notas"
              fullWidth
              multiline
              rows={3}
              value={formData.notes}
              onChange={(e) => setFormData({ ...formData, notes: e.target.value })}
              required
            />
            <TextField
              label="Próximo Passo"
              fullWidth
              value={formData.nextStep}
              onChange={(e) => setFormData({ ...formData, nextStep: e.target.value })}
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
