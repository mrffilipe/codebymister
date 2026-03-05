import { createTheme } from '@mui/material/styles';

// Cores extraídas do projeto institucional code-by-mister
export const theme = createTheme({
  palette: {
    mode: 'dark',
    primary: {
      main: '#4caf50', // Verde Esmeralda
      light: '#a2d9b1', // Verde Oliva Claro
      dark: '#3e8e41', // Tom mais escuro do verde
    },
    secondary: {
      main: '#34495e', // Azul Petróleo
      light: '#2d3e50',
      dark: '#2d3e50',
    },
    error: {
      main: '#d32f2f',
    },
    warning: {
      main: '#e67e22', // Laranja Queimado (usado como highlight)
    },
    info: {
      main: '#a2d9b1',
    },
    success: {
      main: '#4caf50',
    },
    background: {
      default: '#121212', // Preto Suave
      paper: '#1e272e', // Cinza Escuro
    },
    text: {
      primary: '#f1f1f1', // Branco Gelo
      secondary: '#a2d9b1', // Verde Oliva Claro
    },
    divider: '#34495e',
  },
  typography: {
    fontFamily: '"Poppins", "Roboto", "Helvetica", "Arial", sans-serif',
    h1: {
      fontFamily: '"Raleway", "Roboto", sans-serif',
      fontSize: '2.5rem',
      fontWeight: 700,
    },
    h2: {
      fontFamily: '"Raleway", "Roboto", sans-serif',
      fontSize: '2rem',
      fontWeight: 700,
    },
    h3: {
      fontFamily: '"Raleway", "Roboto", sans-serif',
      fontSize: '1.75rem',
      fontWeight: 700,
    },
    h4: {
      fontFamily: '"Raleway", "Roboto", sans-serif',
      fontSize: '1.5rem',
      fontWeight: 700,
    },
    h5: {
      fontFamily: '"Raleway", "Roboto", sans-serif',
      fontSize: '1.25rem',
      fontWeight: 400,
    },
    h6: {
      fontFamily: '"Raleway", "Roboto", sans-serif',
      fontSize: '1rem',
      fontWeight: 400,
    },
    body1: {
      fontFamily: '"Poppins", "Roboto", sans-serif',
      fontWeight: 400,
    },
    body2: {
      fontFamily: '"Poppins", "Roboto", sans-serif',
      fontWeight: 400,
    },
    button: {
      fontFamily: '"Poppins", "Roboto", sans-serif',
      fontWeight: 500,
      textTransform: 'none',
    },
  },
  shape: {
    borderRadius: 8,
  },
  components: {
    MuiCssBaseline: {
      styleOverrides: {
        body: {
          scrollbarColor: '#4caf50 #1e272e',
          '&::-webkit-scrollbar, & *::-webkit-scrollbar': {
            width: 10,
            height: 10,
          },
          '&::-webkit-scrollbar-thumb, & *::-webkit-scrollbar-thumb': {
            borderRadius: 10,
            backgroundColor: '#4caf50',
          },
          '&::-webkit-scrollbar-thumb:hover, & *::-webkit-scrollbar-thumb:hover': {
            backgroundColor: '#3e8e41',
          },
          '&::-webkit-scrollbar-track, & *::-webkit-scrollbar-track': {
            backgroundColor: '#1e272e',
          },
        },
      },
    },
    MuiButton: {
      styleOverrides: {
        root: {
          textTransform: 'none',
          borderRadius: 8,
          fontWeight: 500,
        },
        contained: {
          boxShadow: '0 2px 8px rgba(0,0,0,0.3)',
          '&:hover': {
            boxShadow: '0 4px 12px rgba(0,0,0,0.4)',
          },
        },
      },
    },
    MuiCard: {
      styleOverrides: {
        root: {
          borderRadius: 12,
          boxShadow: '0 2px 8px rgba(0,0,0,0.3)',
          backgroundImage: 'none',
        },
      },
    },
    MuiPaper: {
      styleOverrides: {
        root: {
          backgroundImage: 'none',
        },
      },
    },
    MuiAppBar: {
      styleOverrides: {
        root: {
          backgroundImage: 'none',
          boxShadow: '0 2px 8px rgba(0,0,0,0.3)',
        },
      },
    },
    MuiChip: {
      styleOverrides: {
        root: {
          borderRadius: 6,
        },
      },
    },
  },
});
