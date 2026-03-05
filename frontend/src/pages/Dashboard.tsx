import { useEffect, useState } from 'react';
import {
  Grid,
  Card,
  CardContent,
  Typography,
  Box,
  CircularProgress,
  Paper,
} from '@mui/material';
import {
  TrendingUp,
  People,
  Campaign,
  Description,
  AttachMoney,
} from '@mui/icons-material';
import { PieChart } from '@mui/x-charts/PieChart';
import { BarChart } from '@mui/x-charts/BarChart';
import { LineChart } from '@mui/x-charts/LineChart';
import { dashboardService } from '../services/dashboardService';
import type { DashboardDto } from '../types/dashboard';

export function Dashboard() {
  const [data, setData] = useState<DashboardDto | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      const response = await dashboardService.getData();
      setData(response.data);
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

  const revenueData = [
    { month: 'Jan', value: data.monthlyRevenue * 0.7 },
    { month: 'Fev', value: data.monthlyRevenue * 0.8 },
    { month: 'Mar', value: data.monthlyRevenue * 0.9 },
    { month: 'Abr', value: data.monthlyRevenue },
  ];

  const sourceData = [
    { id: 0, value: 35, label: 'Google' },
    { id: 1, value: 25, label: 'LinkedIn' },
    { id: 2, value: 20, label: 'Indicação' },
    { id: 3, value: 15, label: 'Instagram' },
    { id: 4, value: 5, label: 'Outros' },
  ];

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

      <Grid container spacing={3} sx={{ mt: 1 }}>
        <Grid size={{ xs: 12, md: 4 }}>
          <Paper sx={{ p: 3, height: '100%' }}>
            <Typography variant="h6" gutterBottom>
              Funil de Vendas
            </Typography>
            <Box sx={{ display: 'flex', justifyContent: 'center', mt: 2 }}>
              <PieChart
                series={[
                  {
                    data: pipelineData,
                    highlightScope: { fade: 'global', highlight: 'item' },
                  },
                ]}
                height={280}
              />
            </Box>
          </Paper>
        </Grid>

        <Grid size={{ xs: 12, md: 4 }}>
          <Paper sx={{ p: 3, height: '100%' }}>
            <Typography variant="h6" gutterBottom>
              Receita Mensal
            </Typography>
            <Box sx={{ mt: 2 }}>
              <LineChart
                xAxis={[{ scaleType: 'point', data: revenueData.map(d => d.month) }]}
                series={[
                  {
                    data: revenueData.map(d => d.value),
                    area: true,
                    color: '#4caf50',
                  },
                ]}
                height={280}
                margin={{ top: 10, right: 10, bottom: 30, left: 60 }}
              />
            </Box>
          </Paper>
        </Grid>

        <Grid size={{ xs: 12, md: 4 }}>
          <Paper sx={{ p: 3, height: '100%' }}>
            <Typography variant="h6" gutterBottom>
              Origem dos Leads
            </Typography>
            <Box sx={{ display: 'flex', justifyContent: 'center', mt: 2 }}>
              <BarChart
                xAxis={[{ scaleType: 'band', data: sourceData.map(d => d.label) }]}
                series={[{ data: sourceData.map(d => d.value), color: '#4caf50' }]}
                height={280}
                margin={{ top: 10, right: 10, bottom: 50, left: 40 }}
              />
            </Box>
          </Paper>
        </Grid>
      </Grid>
    </Box>
  );
}
