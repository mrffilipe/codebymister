import { useEffect, useState } from 'react';
import {
  Grid,
  Card,
  CardContent,
  Typography,
  Box,
  CircularProgress,
} from '@mui/material';
import {
  TrendingUp,
  People,
  Campaign,
  Description,
  AttachMoney,
} from '@mui/icons-material';
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

  return (
    <Box>
      <Typography variant="h4" gutterBottom>
        Dashboard
      </Typography>
      <Grid container spacing={3}>
        {cards.map((card, index) => (
          <Grid size={{ xs: 12, sm: 6, md: 4 }} key={index}>
            <Card>
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
    </Box>
  );
}
