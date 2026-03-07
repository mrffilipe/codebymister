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
import { proposalService } from '../services/proposalService';
import { leadsService } from '../services/leadsService';
import type { Proposal, CreateProposalRequest } from '../types/proposal';
import { ProjectType, ProposalAction, ProposalStatus } from '../types/proposal';
import type { LeadDto } from '../types/lead';

const projectTypeLabels: Record<ProjectType, string> = {
  [ProjectType.Website]: 'Website',
  [ProjectType.Ecommerce]: 'E-commerce',
  [ProjectType.LandingPage]: 'Landing Page',
  [ProjectType.System]: 'Sistema',
  [ProjectType.Mobile]: 'Mobile',
  [ProjectType.Integration]: 'Integração',
  [ProjectType.Consulting]: 'Consultoria',
};

const statusLabels: Record<ProposalStatus, string> = {
  [ProposalStatus.Sent]: 'Enviada',
  [ProposalStatus.UnderReview]: 'Em Análise',
  [ProposalStatus.Accepted]: 'Aceita',
  [ProposalStatus.Refused]: 'Recusada',
  [ProposalStatus.Expired]: 'Expirada',
};

const statusColors: Record<ProposalStatus, 'default' | 'info' | 'success' | 'error' | 'warning'> = {
  [ProposalStatus.Sent]: 'default',
  [ProposalStatus.UnderReview]: 'info',
  [ProposalStatus.Accepted]: 'success',
  [ProposalStatus.Refused]: 'error',
  [ProposalStatus.Expired]: 'warning',
};

export function Proposals() {
  const [proposals, setProposals] = useState<Proposal[]>([]);
  const [leads, setLeads] = useState<LeadDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [dialogOpen, setDialogOpen] = useState(false);
  const [editingProposal, setEditingProposal] = useState<Proposal | null>(null);
  const [formData, setFormData] = useState<CreateProposalRequest>({
    leadId: '',
    projectType: ProjectType.Website,
    proposedValue: 0,
    notes: '',
  });

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      const [proposalsRes, leadsRes] = await Promise.all([
        proposalService.getAll(),
        leadsService.getAll(),
      ]);
      setProposals(proposalsRes);
      setLeads(leadsRes.data);
    } catch (error) {
      console.error('Erro ao carregar dados:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleOpenDialog = (proposal?: Proposal) => {
    if (proposal) {
      setEditingProposal(proposal);
      setFormData({
        leadId: proposal.leadId,
        projectType: proposal.projectType,
        proposedValue: proposal.proposedValue,
        notes: proposal.notes || '',
      });
    } else {
      setEditingProposal(null);
      setFormData({
        leadId: '',
        projectType: ProjectType.Website,
        proposedValue: 0,
        notes: '',
      });
    }
    setDialogOpen(true);
  };

  const handleCloseDialog = () => {
    setDialogOpen(false);
    setEditingProposal(null);
  };

  const handleSubmit = async () => {
    try {
      if (editingProposal) {
        await proposalService.update(editingProposal.id, {
          action: ProposalAction.UpdateNotes,
          notes: formData.notes,
        });
      } else {
        await proposalService.create(formData);
      }
      handleCloseDialog();
      loadData();
    } catch (error) {
      console.error('Erro ao salvar proposta:', error);
    }
  };

  const handleDelete = async (id: string) => {
    if (window.confirm('Deseja realmente excluir esta proposta?')) {
      try {
        await proposalService.delete(id);
        loadData();
      } catch (error) {
        console.error('Erro ao excluir proposta:', error);
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
        <Typography variant="h4">Propostas</Typography>
        <Button
          variant="contained"
          startIcon={<Add />}
          onClick={() => handleOpenDialog()}
        >
          Nova Proposta
        </Button>
      </Box>

      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Lead</TableCell>
              <TableCell>Tipo</TableCell>
              <TableCell>Valor</TableCell>
              <TableCell>Status</TableCell>
              <TableCell>Enviada em</TableCell>
              <TableCell align="right">Ações</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {proposals.map((proposal) => (
              <TableRow key={proposal.id}>
                <TableCell>{proposal.leadName}</TableCell>
                <TableCell>{projectTypeLabels[proposal.projectType]}</TableCell>
                <TableCell>
                  {new Intl.NumberFormat('pt-BR', {
                    style: 'currency',
                    currency: 'BRL',
                  }).format(proposal.proposedValue)}
                </TableCell>
                <TableCell>
                  <Chip
                    label={statusLabels[proposal.status]}
                    color={statusColors[proposal.status]}
                    size="small"
                  />
                </TableCell>
                <TableCell>
                  {new Date(proposal.sentAt).toLocaleDateString('pt-BR')}
                </TableCell>
                <TableCell align="right">
                  <IconButton
                    size="small"
                    onClick={() => handleOpenDialog(proposal)}
                  >
                    <Edit />
                  </IconButton>
                  <IconButton
                    size="small"
                    onClick={() => handleDelete(proposal.id)}
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
          {editingProposal ? 'Editar Proposta' : 'Nova Proposta'}
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
              disabled={!!editingProposal}
            >
              {leads.map((lead) => (
                <MenuItem key={lead.id} value={lead.id}>
                  {lead.name}
                </MenuItem>
              ))}
            </TextField>
            <TextField
              label="Tipo de Projeto"
              select
              fullWidth
              value={formData.projectType}
              onChange={(e) => setFormData({ ...formData, projectType: Number(e.target.value) as ProjectType })}
              required
              disabled={!!editingProposal}
            >
              {Object.entries(projectTypeLabels).map(([value, label]) => (
                <MenuItem key={value} value={value}>
                  {label}
                </MenuItem>
              ))}
            </TextField>
            <TextField
              label="Valor Proposto"
              type="number"
              fullWidth
              value={formData.proposedValue}
              onChange={(e) => setFormData({ ...formData, proposedValue: Number(e.target.value) })}
              required
              disabled={!!editingProposal}
            />
            <TextField
              label="Notas"
              fullWidth
              multiline
              rows={3}
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
