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
import { projectService } from '../services/projectService';
import { leadsService } from '../services/leadsService';
import type { Project, CreateProjectRequest } from '../types/project';
import { ProjectStatus } from '../types/project';
import { ProjectType } from '../types/proposal';
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

const statusLabels: Record<ProjectStatus, string> = {
  [ProjectStatus.NotStarted]: 'Não Iniciado',
  [ProjectStatus.InProgress]: 'Em Andamento',
  [ProjectStatus.Testing]: 'Em Testes',
  [ProjectStatus.Delivered]: 'Entregue',
  [ProjectStatus.Completed]: 'Concluído',
  [ProjectStatus.Cancelled]: 'Cancelado',
  [ProjectStatus.OnHold]: 'Em Espera',
};

const statusColors: Record<ProjectStatus, 'default' | 'info' | 'warning' | 'success' | 'error' | 'primary'> = {
  [ProjectStatus.NotStarted]: 'default',
  [ProjectStatus.InProgress]: 'info',
  [ProjectStatus.Testing]: 'warning',
  [ProjectStatus.Delivered]: 'primary',
  [ProjectStatus.Completed]: 'success',
  [ProjectStatus.Cancelled]: 'error',
  [ProjectStatus.OnHold]: 'warning',
};

export function Projects() {
  const [projects, setProjects] = useState<Project[]>([]);
  const [leads, setLeads] = useState<LeadDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [dialogOpen, setDialogOpen] = useState(false);
  const [editingProject, setEditingProject] = useState<Project | null>(null);
  const [formData, setFormData] = useState<CreateProjectRequest>({
    leadId: '',
    projectType: ProjectType.Website,
    closedValue: 0,
    startDate: new Date().toISOString().split('T')[0],
    scopeSummary: '',
    deadline: '',
  });

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      const [projectsRes, leadsRes] = await Promise.all([
        projectService.getAll(),
        leadsService.getAll(),
      ]);
      setProjects(projectsRes);
      setLeads(leadsRes.data);
    } catch (error) {
      console.error('Erro ao carregar dados:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleOpenDialog = (project?: Project) => {
    if (project) {
      setEditingProject(project);
      setFormData({
        leadId: project.leadId,
        projectType: project.projectType,
        closedValue: project.closedValue,
        startDate: project.startDate.split('T')[0],
        scopeSummary: project.scopeSummary,
        deadline: project.deadline ? project.deadline.split('T')[0] : '',
      });
    } else {
      setEditingProject(null);
      setFormData({
        leadId: '',
        projectType: ProjectType.Website,
        closedValue: 0,
        startDate: new Date().toISOString().split('T')[0],
        scopeSummary: '',
        deadline: '',
      });
    }
    setDialogOpen(true);
  };

  const handleCloseDialog = () => {
    setDialogOpen(false);
    setEditingProject(null);
  };

  const handleSubmit = async () => {
    try {
      if (editingProject) {
        await projectService.update(editingProject.id, {
          updateBasicInfo: true,
          projectType: formData.projectType,
          closedValue: formData.closedValue,
          startDate: formData.startDate,
          deadline: formData.deadline || undefined,
          scopeSummary: formData.scopeSummary,
        });
      } else {
        await projectService.create(formData);
      }
      handleCloseDialog();
      loadData();
    } catch (error) {
      console.error('Erro ao salvar projeto:', error);
    }
  };

  const handleDelete = async (id: string) => {
    if (window.confirm('Deseja realmente excluir este projeto?')) {
      try {
        await projectService.delete(id);
        loadData();
      } catch (error) {
        console.error('Erro ao excluir projeto:', error);
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
        <Typography variant="h4">Projetos</Typography>
        <Button
          variant="contained"
          startIcon={<Add />}
          onClick={() => handleOpenDialog()}
        >
          Novo Projeto
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
              <TableCell>Início</TableCell>
              <TableCell align="right">Ações</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {projects.map((project) => (
              <TableRow key={project.id}>
                <TableCell>{project.leadName}</TableCell>
                <TableCell>{projectTypeLabels[project.projectType]}</TableCell>
                <TableCell>
                  {new Intl.NumberFormat('pt-BR', {
                    style: 'currency',
                    currency: 'BRL',
                  }).format(project.closedValue)}
                </TableCell>
                <TableCell>
                  <Chip
                    label={statusLabels[project.status]}
                    color={statusColors[project.status]}
                    size="small"
                  />
                </TableCell>
                <TableCell>
                  {new Date(project.startDate).toLocaleDateString('pt-BR')}
                </TableCell>
                <TableCell align="right">
                  <IconButton
                    size="small"
                    onClick={() => handleOpenDialog(project)}
                  >
                    <Edit />
                  </IconButton>
                  <IconButton
                    size="small"
                    onClick={() => handleDelete(project.id)}
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
          {editingProject ? 'Editar Projeto' : 'Novo Projeto'}
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
              disabled={!!editingProject}
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
            >
              {Object.entries(projectTypeLabels).map(([value, label]) => (
                <MenuItem key={value} value={value}>
                  {label}
                </MenuItem>
              ))}
            </TextField>
            <TextField
              label="Valor Fechado"
              type="number"
              fullWidth
              value={formData.closedValue}
              onChange={(e) => setFormData({ ...formData, closedValue: Number(e.target.value) })}
              required
            />
            <TextField
              label="Data de Início"
              type="date"
              fullWidth
              value={formData.startDate}
              onChange={(e) => setFormData({ ...formData, startDate: e.target.value })}
              required
              InputLabelProps={{ shrink: true }}
            />
            <TextField
              label="Prazo"
              type="date"
              fullWidth
              value={formData.deadline}
              onChange={(e) => setFormData({ ...formData, deadline: e.target.value })}
              InputLabelProps={{ shrink: true }}
            />
            <TextField
              label="Resumo do Escopo"
              fullWidth
              multiline
              rows={3}
              value={formData.scopeSummary}
              onChange={(e) => setFormData({ ...formData, scopeSummary: e.target.value })}
              required
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
