import { Box, Container, Typography, Card, CardContent, CardMedia, Chip, Stack, Button, Divider } from '@mui/material';
import { GitHub as GitHubIcon, Launch as LaunchIcon } from '@mui/icons-material';

const projects = [
  {
    id: 1,
    title: 'Codebymister',
    description: 'Dashboard completo para gestão de leads, projetos, propostas e manutenções. Sistema CRM com autenticação Firebase, interface React e API .NET.',
    technologies: ['.NET 8', 'React', 'TypeScript', 'Firebase', 'Entity Framework Core'],
    image: '/codebymister-project.png',
    repository: null,
    demo: null,
  },
  {
    id: 2,
    title: 'MrDelivery',
    description: 'Sistema completo de gestão para delivery, incluindo cadastro de produtos, controle de custos, precificação inteligente e relatórios financeiros.',
    technologies: ['.NET 9', 'React', 'TypeScript', 'MySQL', 'Material-UI', 'iFood API'],
    image: '/mrdelivery-project.png',
    repository: null,
    demo: null,
  },
  {
    id: 3,
    title: 'Agro Simulator Game',
    description: 'Protótipo de jogo de simulação de fazenda estilo Farming Simulator. Projeto de TCC em Engenharia da Computação com gráficos 3D e mecânicas realistas.',
    technologies: ['Unity 3D', 'C#', 'Blender', '3D Modeling'],
    image: '/agrosimulator-project.png',
    repository: null,
    demo: 'https://www.linkedin.com/in/mrffilipe/',
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
                    {project.repository && (
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
                    )}
                    {project.demo && (
                      <Button
                        variant="contained"
                        size="small"
                        startIcon={<LaunchIcon />}
                        href={project.demo}
                        target="_blank"
                        rel="noopener noreferrer"
                        sx={{ flex: project.repository ? 1 : '100%' }}
                      >
                        {project.demo.includes('linkedin') ? 'LinkedIn' : 'Demo'}
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
