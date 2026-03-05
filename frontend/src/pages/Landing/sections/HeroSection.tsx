import { Box, Container, Typography, Button, Stack } from '@mui/material';
import { ArrowForward as ArrowForwardIcon } from '@mui/icons-material';

export const HeroSection = () => {
  const handleScroll = (id: string) => {
    const element = document.querySelector(id);
    element?.scrollIntoView({ behavior: 'smooth' });
  };

  return (
    <Box
      id="home"
      sx={{
        minHeight: '100vh',
        display: 'flex',
        alignItems: 'center',
        position: 'relative',
        background: 'linear-gradient(180deg, #121212 0%, #1e272e 100%)',
        pt: { xs: 10, md: 12 },
        pb: { xs: 8, md: 10 },
        overflow: 'hidden',
        '&::before': {
          content: '""',
          position: 'absolute',
          top: 0,
          left: 0,
          right: 0,
          bottom: 0,
          backgroundImage: 'url(/background-animation.svg)',
          backgroundSize: 'cover',
          backgroundPosition: 'center',
          opacity: 0.1,
          zIndex: 0,
        },
      }}
    >
      <Container maxWidth="lg" sx={{ position: 'relative', zIndex: 1 }}>
        <Stack spacing={4} alignItems="center" textAlign="center">
          <Typography
            variant="h1"
            sx={{
              fontSize: { xs: '2.5rem', sm: '3.5rem', md: '4.5rem' },
              fontWeight: 700,
              animation: 'slideIn 0.8s ease-out',
              '@keyframes slideIn': {
                from: {
                  opacity: 0,
                  transform: 'translateY(30px)',
                },
                to: {
                  opacity: 1,
                  transform: 'translateY(0)',
                },
              },
            }}
          >
            Olá, sou <Box component="span" sx={{ color: 'primary.main' }}>Filipe</Box>!
          </Typography>

          <Typography
            variant="h5"
            color="text.secondary"
            sx={{
              maxWidth: 800,
              fontSize: { xs: '1.1rem', md: '1.3rem' },
              animation: 'slideIn 0.8s ease-out 0.2s both',
            }}
          >
            Especialista em backend com experiência em frontend, mobile e
            arquitetura de software. Transformo ideias em soluções robustas.
          </Typography>

          <Stack
            direction={{ xs: 'column', sm: 'row' }}
            spacing={2}
            sx={{
              animation: 'slideIn 0.8s ease-out 0.4s both',
            }}
          >
            <Button
              variant="contained"
              size="large"
              endIcon={<ArrowForwardIcon />}
              onClick={() => handleScroll('#projects')}
              sx={{
                px: 4,
                py: 1.5,
                fontSize: '1.1rem',
                boxShadow: '0 4px 14px rgba(76, 175, 80, 0.4)',
                '&:hover': {
                  boxShadow: '0 6px 20px rgba(76, 175, 80, 0.5)',
                  transform: 'translateY(-2px)',
                },
                transition: 'all 0.3s cubic-bezier(0.4, 0, 0.2, 1)',
              }}
            >
              Veja Meus Projetos
            </Button>
            <Button
              variant="outlined"
              size="large"
              onClick={() => handleScroll('#contact')}
              sx={{
                px: 4,
                py: 1.5,
                fontSize: '1.1rem',
                borderWidth: 2,
                borderColor: 'primary.main',
                color: 'primary.main',
                '&:hover': {
                  borderWidth: 2,
                  borderColor: 'primary.light',
                  backgroundColor: 'rgba(76, 175, 80, 0.12)',
                  transform: 'translateY(-2px)',
                },
                transition: 'all 0.3s cubic-bezier(0.4, 0, 0.2, 1)',
              }}
            >
              Entre em Contato
            </Button>
          </Stack>
        </Stack>
      </Container>
    </Box>
  );
};
