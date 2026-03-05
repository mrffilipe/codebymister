import { Box, Container, Typography, Card, CardContent, CardMedia, Chip, Stack, Button, Divider } from '@mui/material';
import { GitHub as GitHubIcon, Launch as LaunchIcon } from '@mui/icons-material';

const projects = [
  {
    id: 1,
    title: 'MesaFlix',
    description: 'Um SaaS para cardápios de estabelecimentos, com pedidos por QR Code e gestão integrada.',
    technologies: ['React', 'Firebase', 'TypeScript'],
    image: 'https://via.placeholder.com/400x300/1e272e/4caf50?text=MesaFlix',
    repository: 'https://github.com/seu-perfil/mesaflix',
    demo: 'https://mesaflix.app',
  },
  {
    id: 2,
    title: 'Simulador de Fazenda',
    description: 'Jogo de simulação de fazenda com gráficos realistas e áreas personalizáveis.',
    technologies: ['Unity 3D', 'C#', 'Blender'],
    image: 'https://via.placeholder.com/400x300/1e272e/4caf50?text=Farm+Simulator',
    repository: 'https://github.com/seu-perfil/farm-simulator',
    demo: 'https://farmsim.demo',
  },
  {
    id: 3,
    title: 'Dashboard Analytics',
    description: 'Sistema de análise de dados com visualizações interativas e relatórios personalizados.',
    technologies: ['React', 'TypeScript', '.NET Core'],
    image: 'https://via.placeholder.com/400x300/1e272e/4caf50?text=Dashboard',
    repository: 'https://github.com/seu-perfil/dashboard',
    demo: null,
  },
];

export const ProjectsSection = () => {
  return (
    <Box
      id="projects"
      sx={{
        py: { xs: 8, md: 12 },
        backgroundColor: '#1a2228',
        position: 'relative',
        '&::before': {
          content: '""',
          position: 'absolute',
          top: 0,
          left: 0,
          right: 0,
          bottom: 0,
          background: 'radial-gradient(ellipse at 20% 80%, rgba(76, 175, 80, 0.06) 0%, transparent 60%)',
          pointerEvents: 'none',
        },
      }}
    >
      <Container maxWidth="lg" sx={{ position: 'relative', zIndex: 1 }}>
        <Typography
          variant="h2"
          align="center"
          gutterBottom
          sx={{ color: 'primary.main', mb: 6 }}
        >
          Projetos
        </Typography>

        <Box
          sx={{
            display: 'grid',
            gridTemplateColumns: {
              xs: '1fr',
              md: 'repeat(2, 1fr)',
              lg: 'repeat(3, 1fr)',
            },
            gap: { xs: 3, md: 4, lg: 5 },
          }}
        >
          {projects.map((project) => (
            <Box key={project.id}>
              <Card
                sx={{
                  height: '100%',
                  display: 'flex',
                  flexDirection: 'column',
                  transition: 'all 0.3s cubic-bezier(0.4, 0, 0.2, 1)',
                  boxShadow: '0 4px 12px rgba(0,0,0,0.3)',
                  '&:hover': {
                    transform: 'translateY(-12px)',
                    boxShadow: '0 16px 32px rgba(76, 175, 80, 0.25)',
                  },
                }}
              >
                <CardMedia
                  component="img"
                  height="200"
                  image={project.image}
                  alt={project.title}
                  sx={{ backgroundColor: 'background.default' }}
                />
                <CardContent sx={{ flexGrow: 1, display: 'flex', flexDirection: 'column' }}>
                  <Typography variant="h5" gutterBottom sx={{ color: 'primary.light' }}>
                    {project.title}
                  </Typography>
                  <Typography variant="body2" color="text.secondary" paragraph sx={{ flexGrow: 1 }}>
                    {project.description}
                  </Typography>

                  <Stack direction="row" spacing={1} flexWrap="wrap" useFlexGap sx={{ mb: 2.5 }}>
                    {project.technologies.map((tech) => (
                      <Chip
                        key={tech}
                        label={tech}
                        size="small"
                        sx={{
                          backgroundColor: 'primary.main',
                          color: 'background.default',
                          fontSize: '0.75rem',
                          height: 26,
                          px: 0.5,
                        }}
                      />
                    ))}
                  </Stack>

                  <Divider sx={{ mb: 2 }} />

                  <Stack direction="row" spacing={2}>
                    <Button
                      variant="outlined"
                      size="small"
                      startIcon={<GitHubIcon />}
                      href={project.repository}
                      target="_blank"
                      rel="noopener noreferrer"
                      sx={{ flex: 1 }}
                    >
                      Código
                    </Button>
                    {project.demo && (
                      <Button
                        variant="contained"
                        size="small"
                        startIcon={<LaunchIcon />}
                        href={project.demo}
                        target="_blank"
                        rel="noopener noreferrer"
                        sx={{ flex: 1 }}
                      >
                        Demo
                      </Button>
                    )}
                  </Stack>
                </CardContent>
              </Card>
            </Box>
          ))}
        </Box>
      </Container>
    </Box>
  );
};
