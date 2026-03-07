import { useEffect, useState } from 'react';
import {
  Grid,
  Card,
  CardContent,
  Typography,
  Box,
  CircularProgress,
  Paper,
  Stack,
  Chip,
} from '@mui/material';
import {
  TrendingUp,
  People,
  Campaign,
  Description,
  AttachMoney,
  Google,
  LinkedIn,
  Instagram,
  PersonAdd,
  MoreHoriz,
} from '@mui/icons-material';
import { PieChart } from '@mui/x-charts/PieChart';
import { BarChart } from '@mui/x-charts/BarChart';
import { LineChart } from '@mui/x-charts/LineChart';
import { dashboardService } from '../services/dashboardService';
import { leadsService } from '../services/leadsService';
import type { DashboardDto } from '../types/dashboard';
import type { LeadDto } from '../types/lead';
import { LeadSource } from '../types/lead';

export function Dashboard() {
  const [data, setData] = useState<DashboardDto | null>(null);
  const [leads, setLeads] = useState<LeadDto[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      const [dashboardResponse, leadsResponse] = await Promise.all([
        dashboardService.getData(),
        leadsService.getAll(),
      ]);
      setData(dashboardResponse.data);
      setLeads(leadsResponse.data);
    } catch (error) {
      console.error('Erro ao carregar dashboard:', error);
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return (
      <Box display="flex" justifyContent="center" alignItems="center" minHeight="400px">
        <CircularProgress />
      </Box>
    );
  }

  if (!data) {
    return <Typography>Erro ao carregar dados</Typography>;
  }

  const cards = [
    {
      title: 'Total de Leads',
      value: data.totalLeads,
      icon: <People fontSize="large" />,
      color: '#1976d2',
    },
    {
      title: 'Abordagens',
      value: data.totalOutreach,
      icon: <Campaign fontSize="large" />,
      color: '#9c27b0',
    },
    {
      title: 'Propostas',
      value: data.totalProposals,
      icon: <Description fontSize="large" />,
      color: '#ed6c02',
    },
    {
      title: 'Projetos Fechados',
      value: data.closedProjects,
      icon: <TrendingUp fontSize="large" />,
      color: '#2e7d32',
    },
    {
      title: 'Taxa de Conversão',
      value: `${data.conversionRate.toFixed(1)}%`,
      icon: <TrendingUp fontSize="large" />,
      color: '#0288d1',
    },
    {
      title: 'Ticket Médio',
      value: `R$ ${data.averageTicket.toLocaleString('pt-BR', { minimumFractionDigits: 2 })}`,
      icon: <AttachMoney fontSize="large" />,
      color: '#388e3c',
    },
    {
      title: 'Receita Mensal',
      value: `R$ ${data.monthlyRevenue.toLocaleString('pt-BR', { minimumFractionDigits: 2 })}`,
      icon: <AttachMoney fontSize="large" />,
      color: '#1976d2',
    },
    {
      title: 'Receita Recorrente',
      value: `R$ ${data.recurringRevenue.toLocaleString('pt-BR', { minimumFractionDigits: 2 })}`,
      icon: <AttachMoney fontSize="large" />,
      color: '#7b1fa2',
    },
    {
      title: 'Receita Prevista',
      value: `R$ ${data.forecastRevenue.toLocaleString('pt-BR', { minimumFractionDigits: 2 })}`,
      icon: <AttachMoney fontSize="large" />,
      color: '#f57c00',
    },
  ];

  const pipelineData = [
    { id: 0, value: data.totalLeads, label: 'Leads' },
    { id: 1, value: data.totalOutreach, label: 'Abordagens' },
    { id: 2, value: data.totalProposals, label: 'Propostas' },
    { id: 3, value: data.closedProjects, label: 'Fechados' },
  ];

  const hasPipelineData = pipelineData.some(item => item.value > 0);

  const revenueData = [
    { month: 'Jan', value: data.monthlyRevenue * 0.7 },
    { month: 'Fev', value: data.monthlyRevenue * 0.8 },
    { month: 'Mar', value: data.monthlyRevenue * 0.9 },
    { month: 'Abr', value: data.monthlyRevenue },
  ];

  const getLeadCountBySource = (source: LeadSource): number => {
    return leads.filter(lead => lead.source === source).length;
  };

  const sourceData = [
    { id: 0, value: getLeadCountBySource(LeadSource.GoogleSearch), label: 'Google', color: '#4285F4', icon: <Google /> },
    { id: 1, value: getLeadCountBySource(LeadSource.LinkedIn), label: 'LinkedIn', color: '#0A66C2', icon: <LinkedIn /> },
    { id: 2, value: getLeadCountBySource(LeadSource.Referral), label: 'Indicação', color: '#4CAF50', icon: <PersonAdd /> },
    { id: 3, value: getLeadCountBySource(LeadSource.Instagram), label: 'Instagram', color: '#E4405F', icon: <Instagram /> },
    { id: 4, value: getLeadCountBySource(LeadSource.Other), label: 'Outros', color: '#9E9E9E', icon: <MoreHoriz /> },
  ];

  const hasRevenueData = data.monthlyRevenue > 0;
  const hasSourceData = sourceData.some(item => item.value > 0);

  return (
    <Box>
      <Typography variant="h4" gutterBottom sx={{ mb: 3 }}>
        Dashboard
      </Typography>
      
      <Grid container spacing={3}>
        {cards.map((card, index) => (
          <Grid size={{ xs: 12, sm: 6, md: 4 }} key={index}>
            <Card sx={{ height: '100%' }}>
              <CardContent>
                <Box display="flex" alignItems="center" justifyContent="space-between">
                  <Box>
                    <Typography color="text.secondary" gutterBottom variant="body2">
                      {card.title}
                    </Typography>
                    <Typography variant="h5" component="div">
                      {card.value}
                    </Typography>
                  </Box>
                  <Box sx={{ color: card.color }}>{card.icon}</Box>
                </Box>
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>

      <Grid container spacing={3} sx={{ mt: { xs: 3, md: 4 } }}>
        <Grid size={{ xs: 12, md: 4 }}>
          <Paper sx={{ p: 3, height: '100%' }}>
            <Typography variant="h6" gutterBottom>
              Funil de Vendas
            </Typography>
            {hasPipelineData ? (
              <Box sx={{ display: 'flex', justifyContent: 'center', mt: 2 }}>
                <PieChart
                  series={[
                    {
                      data: pipelineData,
                      highlightScope: { fade: 'global', highlight: 'item' },
                      arcLabel: (item) => `${item.value}`,
                      arcLabelMinAngle: 35,
                    },
                  ]}
                  height={280}
                />
              </Box>
            ) : (
              <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: 280, mt: 2 }}>
                <Typography variant="body2" color="text.secondary">
                  Não há dados disponíveis
                </Typography>
              </Box>
            )}
          </Paper>
        </Grid>

        <Grid size={{ xs: 12, md: 4 }}>
          <Paper sx={{ p: 3, height: '100%' }}>
            <Typography variant="h6" gutterBottom>
              Receita Mensal
            </Typography>
            {hasRevenueData ? (
              <Box sx={{ mt: 2 }}>
                <LineChart
                  xAxis={[{ scaleType: 'point', data: revenueData.map(d => d.month) }]}
                  series={[
                    {
                      data: revenueData.map(d => d.value),
                      area: true,
                      color: '#4caf50',
                      label: 'Receita (R$)',
                      showMark: true,
                    },
                  ]}
                  height={280}
                  margin={{ top: 10, right: 10, bottom: 30, left: 60 }}
                />
              </Box>
            ) : (
              <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: 280, mt: 2 }}>
                <Typography variant="body2" color="text.secondary">
                  Não há dados disponíveis
                </Typography>
              </Box>
            )}
          </Paper>
        </Grid>

        <Grid size={{ xs: 12, md: 4 }}>
          <Paper sx={{ p: 3, height: '100%' }}>
            <Typography variant="h6" gutterBottom>
              Origem dos Leads
            </Typography>
            {hasSourceData ? (
              <Box>
                <Box sx={{ display: 'flex', justifyContent: 'center', mt: 2 }}>
                  <BarChart
                    xAxis={[{ scaleType: 'band', data: sourceData.map(d => d.label) }]}
                    series={sourceData.map((source) => ({
                      data: sourceData.map((s) => s.id === source.id ? s.value : 0),
                      stack: 'total',
                      color: source.color,
                    }))}
                    height={240}
                    margin={{ top: 10, right: 10, bottom: 50, left: 40 }}
                  />
                </Box>
                <Stack direction="row" spacing={1} flexWrap="wrap" sx={{ mt: 1, justifyContent: 'center', gap: 1 }}>
                  {sourceData.map((source) => (
                    <Chip
                      key={source.id}
                      icon={source.icon}
                      label={`${source.label}: ${source.value}`}
                      size="small"
                      sx={{
                        backgroundColor: `${source.color}20`,
                        color: source.color,
                        border: `1px solid ${source.color}40`,
                        '& .MuiChip-icon': {
                          color: source.color,
                        },
                      }}
                    />
                  ))}
                </Stack>
              </Box>
            ) : (
              <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: 280, mt: 2 }}>
                <Typography variant="body2" color="text.secondary">
                  Não há dados disponíveis
                </Typography>
              </Box>
            )}
          </Paper>
        </Grid>
      </Grid>
    </Box>
  );
}
