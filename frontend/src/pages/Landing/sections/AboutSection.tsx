import { Box, Container, Typography, Paper, Stack } from '@mui/material';

export const AboutSection = () => {
  return (
    <Box
      id="about"
      sx={{
        py: { xs: 8, md: 12 },
        backgroundColor: '#0f1419',
        position: 'relative',
        '&::before': {
          content: '""',
          position: 'absolute',
          top: 0,
          left: 0,
          right: 0,
          bottom: 0,
          background: 'radial-gradient(circle at 80% 20%, rgba(76, 175, 80, 0.08) 0%, transparent 50%)',
          pointerEvents: 'none',
        },
      }}
    >
      <Container maxWidth="lg" sx={{ position: 'relative', zIndex: 1 }}>
        <Stack 
          direction={{ xs: 'column', md: 'row' }} 
          spacing={6} 
          alignItems="center"
        >
          <Box sx={{ flex: { md: '0 0 40%' }, width: '100%' }}>
            <Box
              sx={{
                width: '100%',
                maxWidth: 350,
                mx: 'auto',
                aspectRatio: '1',
                borderRadius: '50%',
                overflow: 'hidden',
                boxShadow: '0 12px 32px rgba(76, 175, 80, 0.3)',
                border: 4,
                borderColor: 'primary.main',
                position: 'relative',
                transition: 'transform 0.3s ease, box-shadow 0.3s ease',
                '&:hover': {
                  transform: 'scale(1.05)',
                  boxShadow: '0 16px 40px rgba(76, 175, 80, 0.4)',
                },
                '&::after': {
                  content: '""',
                  position: 'absolute',
                  top: 0,
                  left: 0,
                  right: 0,
                  bottom: 0,
                  borderRadius: '50%',
                  background: 'linear-gradient(135deg, rgba(76, 175, 80, 0.2) 0%, transparent 50%)',
                  pointerEvents: 'none',
                },
              }}
            >
              <Box
                component="img"
                src="/profile-picture.jpeg"
                alt="Filipe"
                sx={{
                  width: '100%',
                  height: '100%',
                  objectFit: 'cover',
                }}
                onError={(e) => {
                  (e.target as HTMLImageElement).src = 'https://via.placeholder.com/400x400/1e272e/4caf50?text=Filipe';
                }}
              />
            </Box>
          </Box>

          <Box sx={{ flex: 1 }}>
            <Typography variant="h2" gutterBottom sx={{ color: 'primary.main' }}>
              Sobre Mim
            </Typography>

            <Typography variant="body1" paragraph sx={{ fontSize: '1.1rem', lineHeight: 1.8 }}>
              Olá! Meu nome é Filipe, sou um Engenheiro de Software apaixonado por
              tecnologia e inovação. Minha jornada começou em 2015 como{' '}
              <Box component="span" sx={{ color: 'warning.main', fontWeight: 500 }}>entusiasta de tecnologia</Box>,
              explorando linguagens como{' '}
              <Box component="span" sx={{ color: 'warning.main', fontWeight: 500 }}>C#</Box> e ferramentas como{' '}
              <Box component="span" sx={{ color: 'warning.main', fontWeight: 500 }}>Unity3D</Box>. Desde então,
              construí uma base sólida em{' '}
              <Box component="span" sx={{ color: 'warning.main', fontWeight: 500 }}>engenharia de software</Box>,
              com experiência em{' '}
              <Box component="span" sx={{ color: 'warning.main', fontWeight: 500 }}>desenvolvimento full-stack</Box>,{' '}
              <Box component="span" sx={{ color: 'warning.main', fontWeight: 500 }}>arquitetura de sistemas</Box>, e muito mais.
            </Typography>

            <Typography variant="body1" paragraph sx={{ fontSize: '1.1rem', lineHeight: 1.8 }}>
              Atualmente, meu foco está em dominar tecnologias modernas como{' '}
              <Box component="span" sx={{ color: 'warning.main', fontWeight: 500 }}>.NET 9</Box>,{' '}
              <Box component="span" sx={{ color: 'warning.main', fontWeight: 500 }}>TypeScript</Box>,{' '}
              <Box component="span" sx={{ color: 'warning.main', fontWeight: 500 }}>React</Box>, e{' '}
              <Box component="span" sx={{ color: 'warning.main', fontWeight: 500 }}>Node.js</Box>. Além de atuar com
              desenvolvimento mobile utilizando{' '}
              <Box component="span" sx={{ color: 'warning.main', fontWeight: 500 }}>Flutter</Box>,{' '}
              <Box component="span" sx={{ color: 'warning.main', fontWeight: 500 }}>React Native</Box>, e{' '}
              <Box component="span" sx={{ color: 'warning.main', fontWeight: 500 }}>Kotlin Multiplatform</Box>. Meu
              objetivo é transformar ideias em soluções inovadoras e robustas.
            </Typography>

            <Paper
              elevation={0}
              sx={{
                mt: 4,
                p: 3,
                backgroundColor: 'background.paper',
                borderLeft: 4,
                borderColor: 'primary.main',
              }}
            >
              <Typography variant="h5" gutterBottom sx={{ color: 'primary.light' }}>
                Objetivos Futuros
              </Typography>
              <Typography variant="body1" sx={{ fontSize: '1.1rem', lineHeight: 1.8 }}>
                Formado em{' '}
                <Box component="span" sx={{ color: 'warning.main', fontWeight: 500 }}>
                  Engenharia da Computação
                </Box>
                , estou comprometido em continuar aprofundando meus conhecimentos
                e explorando tecnologias emergentes que possam transformar a forma como
                desenvolvemos software e criamos experiências digitais impactantes.
              </Typography>
            </Paper>
          </Box>
        </Stack>
      </Container>
    </Box>
  );
};
