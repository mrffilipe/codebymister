import { Box, Typography, Button, TableContainer, Paper, Table, useMediaQuery, useTheme } from '@mui/material';
import type { ReactNode } from 'react';

interface ResponsiveTableLayoutProps {
  title: string;
  buttonLabel: string;
  buttonIcon: ReactNode;
  onButtonClick: () => void;
  children: ReactNode;
}

export function ResponsiveTableLayout({
  title,
  buttonLabel,
  buttonIcon,
  onButtonClick,
  children,
}: ResponsiveTableLayoutProps) {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('md'));

  return (
    <Box sx={{ width: '100%', overflowX: 'hidden' }}>
      <Box display="flex" justifyContent="space-between" alignItems="center" mb={3}>
        <Typography variant="h4" sx={{ fontSize: { xs: '1.5rem', sm: '2rem' } }}>
          {title}
        </Typography>
        <Button
          variant="contained"
          startIcon={!isMobile && buttonIcon}
          onClick={onButtonClick}
          size={isMobile ? 'small' : 'medium'}
        >
          {isMobile ? buttonLabel.split(' ')[0] : buttonLabel}
        </Button>
      </Box>

      <TableContainer component={Paper} sx={{ width: '100%', overflowX: 'auto' }}>
        <Table sx={{ minWidth: 650 }}>{children}</Table>
      </TableContainer>
    </Box>
  );
}
