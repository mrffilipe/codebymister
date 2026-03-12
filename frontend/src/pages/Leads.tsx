import { useEffect, useState } from 'react';
import {
  Box,
  Button,
  Typography,
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
  useMediaQuery,
  useTheme,
  FormControlLabel,
  Checkbox,
  TableSortLabel,
} from '@mui/material';
import { Add, Edit, Delete } from '@mui/icons-material';
import { leadsService } from '../services/leadsService';
import type { LeadDto, CreateLeadRequest, LeadPriority, LeadSource } from '../types/lead';
import { StepperDialog } from '../components/StepperDialog';
import { phoneMask, removeMask, instagramMask } from '../utils/masks';

type SortDirection = 'asc' | 'desc';
type SortField = 'name' | 'segment' | 'city' | 'priority' | 'source' | 'alreadyApproached' | 'createdAt';

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
  const [alreadyApproached, setAlreadyApproached] = useState(false);
  const [sortField, setSortField] = useState<SortField>('name');
  const [sortDirection, setSortDirection] = useState<SortDirection>('asc');

  const handleSort = (field: SortField) => {
    const isAsc = sortField === field && sortDirection === 'asc';
    setSortDirection(isAsc ? 'desc' : 'asc');
    setSortField(field);
  };

  const sortLeads = (leadsToSort: LeadDto[]) => {
    return [...leadsToSort].sort((a, b) => {
      let aValue: any;
      let bValue: any;

      switch (sortField) {
        case 'name':
          aValue = a.name.toLowerCase();
          bValue = b.name.toLowerCase();
          break;
        case 'segment':
          aValue = a.segment.toLowerCase();
          bValue = b.segment.toLowerCase();
          break;
        case 'city':
          aValue = a.city.toLowerCase();
          bValue = b.city.toLowerCase();
          break;
        case 'priority':
          aValue = a.priority;
          bValue = b.priority;
          break;
        case 'source':
          aValue = a.source;
          bValue = b.source;
          break;
        case 'alreadyApproached':
          aValue = a.alreadyApproached;
          bValue = b.alreadyApproached;
          break;
        case 'createdAt':
          aValue = new Date(a.createdAt);
          bValue = new Date(b.createdAt);
          break;
        default:
          aValue = a.name.toLowerCase();
          bValue = b.name.toLowerCase();
      }

      if (aValue < bValue) {
        return sortDirection === 'asc' ? -1 : 1;
      }
      if (aValue > bValue) {
        return sortDirection === 'asc' ? 1 : -1;
      }
      return 0;
    });
  };

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
      setAlreadyApproached(lead.alreadyApproached);
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
      setAlreadyApproached(false);
    }
    setDialogOpen(true);
  };

  const handleCloseDialog = () => {
    setDialogOpen(false);
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
    setAlreadyApproached(false);
  };

  const handleSubmit = async () => {
    const dataToSend = {
      ...formData,
      phone: formData.phone ? removeMask(formData.phone) : '',
      instagram: formData.instagram ? formData.instagram.replace('@', '') : '',
      alreadyApproached,
    };

    if (editingLead) {
      await leadsService.update(editingLead.id, dataToSend);
    } else {
      await leadsService.create(dataToSend);
    }
    handleCloseDialog();
    loadLeads();
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

  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('md'));

  if (loading) {
    return (
      <Box display="flex" justifyContent="center" alignItems="center" minHeight="400px">
        <CircularProgress />
      </Box>
    );
  }

  return (
    <Box sx={{ width: '100%', overflowX: 'hidden' }}>
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={3}>
        <Typography variant="h4" sx={{ fontSize: { xs: '1.5rem', sm: '2rem' } }}>
          Leads
        </Typography>
        <Button
          variant="contained"
          startIcon={!isMobile && <Add />}
          onClick={() => handleOpenDialog()}
          size={isMobile ? 'small' : 'medium'}
        >
          {isMobile ? 'Novo' : 'Novo Lead'}
        </Button>
      </Box>

      <TableContainer component={Paper} sx={{ width: '100%', overflowX: 'auto' }}>
        <Table sx={{ minWidth: 650 }}>
          <TableHead>
            <TableRow>
              <TableCell>
                <TableSortLabel
                  active={sortField === 'name'}
                  direction={sortField === 'name' ? sortDirection : 'asc'}
                  onClick={() => handleSort('name')}
                >
                  Nome
                </TableSortLabel>
              </TableCell>
              <TableCell>
                <TableSortLabel
                  active={sortField === 'segment'}
                  direction={sortField === 'segment' ? sortDirection : 'asc'}
                  onClick={() => handleSort('segment')}
                >
                  Segmento
                </TableSortLabel>
              </TableCell>
              <TableCell>
                <TableSortLabel
                  active={sortField === 'city'}
                  direction={sortField === 'city' ? sortDirection : 'asc'}
                  onClick={() => handleSort('city')}
                >
                  Cidade
                </TableSortLabel>
              </TableCell>
              <TableCell>
                <TableSortLabel
                  active={sortField === 'priority'}
                  direction={sortField === 'priority' ? sortDirection : 'asc'}
                  onClick={() => handleSort('priority')}
                >
                  Prioridade
                </TableSortLabel>
              </TableCell>
              <TableCell>
                <TableSortLabel
                  active={sortField === 'source'}
                  direction={sortField === 'source' ? sortDirection : 'asc'}
                  onClick={() => handleSort('source')}
                >
                  Origem
                </TableSortLabel>
              </TableCell>
              <TableCell>
                <TableSortLabel
                  active={sortField === 'alreadyApproached'}
                  direction={sortField === 'alreadyApproached' ? sortDirection : 'asc'}
                  onClick={() => handleSort('alreadyApproached')}
                >
                  Já Abordado
                </TableSortLabel>
              </TableCell>
              <TableCell>
                <TableSortLabel
                  active={sortField === 'createdAt'}
                  direction={sortField === 'createdAt' ? sortDirection : 'asc'}
                  onClick={() => handleSort('createdAt')}
                >
                  Criado em
                </TableSortLabel>
              </TableCell>
              <TableCell align="right">Ações</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {sortLeads(leads).map((lead) => (
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
                  <Chip
                    label={lead.alreadyApproached ? 'Sim' : 'Não'}
                    color={lead.alreadyApproached ? 'success' : 'default'}
                    size="small"
                  />
                </TableCell>
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

      <StepperDialog
        open={dialogOpen}
        title={editingLead ? 'Editar Lead' : 'Novo Lead'}
        steps={['Informações Básicas', 'Detalhes', 'Contato']}
        onClose={handleCloseDialog}
        onSave={handleSubmit}
      >
        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
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
        </Box>

        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
          <TextField
            label="Descrição do Problema"
            fullWidth
            multiline
            rows={4}
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
          <FormControlLabel
            control={
              <Checkbox
                checked={alreadyApproached}
                onChange={(e) => setAlreadyApproached(e.target.checked)}
                color="primary"
              />
            }
            label="Já Abordado"
          />
        </Box>

        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
          <TextField
            label="Telefone"
            fullWidth
            value={formData.phone}
            onChange={(e) => setFormData({ ...formData, phone: phoneMask(e.target.value) })}
            placeholder="(00) 00000-0000"
            inputProps={{ maxLength: 15 }}
          />
          <TextField
            label="Instagram"
            fullWidth
            value={formData.instagram}
            onChange={(e) => setFormData({ ...formData, instagram: instagramMask(e.target.value) })}
            placeholder="@usuario"
          />
          <TextField
            label="Website"
            fullWidth
            type="url"
            value={formData.website}
            onChange={(e) => setFormData({ ...formData, website: e.target.value })}
            placeholder="https://exemplo.com"
          />
        </Box>
      </StepperDialog>
    </Box>
  );
}
