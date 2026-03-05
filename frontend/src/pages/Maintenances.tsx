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
import { maintenanceService } from '../services/maintenanceService';
import { projectService } from '../services/projectService';
import type { Maintenance, CreateMaintenanceRequest } from '../types/maintenance';
import { MaintenanceStatus } from '../types/maintenance';
import type { Project } from '../types/project';

const statusLabels: Record<MaintenanceStatus, string> = {
  [MaintenanceStatus.Active]: 'Ativa',
  [MaintenanceStatus.Suspended]: 'Suspensa',
  [MaintenanceStatus.Cancelled]: 'Cancelada',
  [MaintenanceStatus.Expired]: 'Expirada',
};

const statusColors: Record<MaintenanceStatus, 'success' | 'warning' | 'error' | 'default'> = {
  [MaintenanceStatus.Active]: 'success',
  [MaintenanceStatus.Suspended]: 'warning',
  [MaintenanceStatus.Cancelled]: 'error',
  [MaintenanceStatus.Expired]: 'default',
};

export function Maintenances() {
  const [maintenances, setMaintenances] = useState<Maintenance[]>([]);
  const [projects, setProjects] = useState<Project[]>([]);
  const [loading, setLoading] = useState(true);
  const [dialogOpen, setDialogOpen] = useState(false);
  const [editingMaintenance, setEditingMaintenance] = useState<Maintenance | null>(null);
  const [formData, setFormData] = useState<CreateMaintenanceRequest>({
    projectId: '',
    monthlyValue: 0,
    startDate: new Date().toISOString().split('T')[0],
    hostingIncluded: false,
    notes: '',
  });

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      const [maintenancesRes, projectsRes] = await Promise.all([
        maintenanceService.getAll(),
        projectService.getAll(),
      ]);
      setMaintenances(maintenancesRes);
      setProjects(projectsRes);
    } catch (error) {
      console.error('Erro ao carregar dados:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleOpenDialog = (maintenance?: Maintenance) => {
    if (maintenance) {
      setEditingMaintenance(maintenance);
      setFormData({
        projectId: maintenance.projectId,
        monthlyValue: maintenance.monthlyValue,
        startDate: maintenance.startDate.split('T')[0],
        hostingIncluded: maintenance.hostingIncluded,
        notes: maintenance.notes || '',
      });
    } else {
      setEditingMaintenance(null);
      setFormData({
        projectId: '',
        monthlyValue: 0,
        startDate: new Date().toISOString().split('T')[0],
        hostingIncluded: false,
        notes: '',
      });
    }
    setDialogOpen(true);
  };

  const handleCloseDialog = () => {
    setDialogOpen(false);
    setEditingMaintenance(null);
  };

  const handleSubmit = async () => {
    try {
      if (editingMaintenance) {
        await maintenanceService.update(editingMaintenance.id, {
          updateBasicInfo: true,
          monthlyValue: formData.monthlyValue,
          hostingIncluded: formData.hostingIncluded,
          notes: formData.notes,
        });
      } else {
        await maintenanceService.create(formData);
      }
      handleCloseDialog();
      loadData();
    } catch (error) {
      console.error('Erro ao salvar manutenção:', error);
    }
  };

  const handleDelete = async (id: string) => {
    if (window.confirm('Deseja realmente excluir esta manutenção?')) {
      try {
        await maintenanceService.delete(id);
        loadData();
      } catch (error) {
        console.error('Erro ao excluir manutenção:', error);
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
        <Typography variant="h4">Manutenções</Typography>
        <Button
          variant="contained"
          startIcon={<Add />}
          onClick={() => handleOpenDialog()}
        >
          Nova Manutenção
        </Button>
      </Box>

      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Projeto</TableCell>
              <TableCell>Valor Mensal</TableCell>
              <TableCell>Status</TableCell>
              <TableCell>Hospedagem</TableCell>
              <TableCell>Próxima Cobrança</TableCell>
              <TableCell align="right">Ações</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {maintenances.map((maintenance) => (
              <TableRow key={maintenance.id}>
                <TableCell>{maintenance.projectName}</TableCell>
                <TableCell>
                  {new Intl.NumberFormat('pt-BR', {
                    style: 'currency',
                    currency: 'BRL',
                  }).format(maintenance.monthlyValue)}
                </TableCell>
                <TableCell>
                  <Chip
                    label={statusLabels[maintenance.status]}
                    color={statusColors[maintenance.status]}
                    size="small"
                  />
                </TableCell>
                <TableCell>
                  <Chip
                    label={maintenance.hostingIncluded ? 'Sim' : 'Não'}
                    color={maintenance.hostingIncluded ? 'success' : 'default'}
                    size="small"
                  />
                </TableCell>
                <TableCell>
                  {new Date(maintenance.nextBillingDate).toLocaleDateString('pt-BR')}
                </TableCell>
                <TableCell align="right">
                  <IconButton
                    size="small"
                    onClick={() => handleOpenDialog(maintenance)}
                  >
                    <Edit />
                  </IconButton>
                  <IconButton
                    size="small"
                    onClick={() => handleDelete(maintenance.id)}
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
          {editingMaintenance ? 'Editar Manutenção' : 'Nova Manutenção'}
        </DialogTitle>
        <DialogContent>
          <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2, mt: 2 }}>
            <TextField
              label="Projeto"
              select
              fullWidth
              value={formData.projectId}
              onChange={(e) => setFormData({ ...formData, projectId: e.target.value })}
              required
              disabled={!!editingMaintenance}
            >
              {projects.map((project) => (
                <MenuItem key={project.id} value={project.id}>
                  {project.leadName} - {project.scopeSummary.substring(0, 50)}
                </MenuItem>
              ))}
            </TextField>
            <TextField
              label="Valor Mensal"
              type="number"
              fullWidth
              value={formData.monthlyValue}
              onChange={(e) => setFormData({ ...formData, monthlyValue: Number(e.target.value) })}
              required
            />
            <TextField
              label="Data de Início"
              type="date"
              fullWidth
              value={formData.startDate}
              onChange={(e) => setFormData({ ...formData, startDate: e.target.value })}
              required
              disabled={!!editingMaintenance}
              InputLabelProps={{ shrink: true }}
            />
            <TextField
              label="Hospedagem Incluída"
              select
              fullWidth
              value={formData.hostingIncluded ? 'true' : 'false'}
              onChange={(e) => setFormData({ ...formData, hostingIncluded: e.target.value === 'true' })}
            >
              <MenuItem value="true">Sim</MenuItem>
              <MenuItem value="false">Não</MenuItem>
            </TextField>
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
