import { Box, Container, Typography, Paper, Chip, Stack } from '@mui/material';
import { Timeline, TimelineItem, TimelineSeparator, TimelineConnector, TimelineContent, TimelineDot, TimelineOppositeContent } from '@mui/lab';
import { Work as WorkIcon } from '@mui/icons-material';

const experiences = [
  {
    company: 'Desenvolvedor Backend',
    role: 'Freelancer',
    period: 'Fev 2021 - Atual',
    description:
      'Como freelancer, aprimorei minhas habilidades em liderança e comunicação, trabalhando com uma ampla gama de tecnologias em projetos diversificados.',
    technologies: [
      'C#',
      '.NET Core',
      'TypeScript',
      'React',
      'NextJS',
      'C/C++',
      'MySQL',
      'MongoDB',
      'GIT',
      'Docker',
      'RabbitMQ',
    ],
  },
  {
    company: 'Desenvolvedor Autodidata',
    role: 'Autodidata',
    period: 'Abr 2015 - Dez 2020',
    description:
      'Iniciei minha jornada na programação com foco em C# e Unity3D, desenvolvendo jogos e aplicações interativas. Posteriormente, ampliei minha base técnica para o desenvolvimento web, aprendendo HTML, CSS, JavaScript e introduzindo-me ao Node.js.',
    technologies: [
      'C#',
      'Unity3D',
      'HTML',
      'CSS',
      'JavaScript',
      'Node.js',
      'PHP',
    ],
  },
];

export const ExperienceSection = () => {
  return (
    <Box
      id="experience"
      sx={{
        py: { xs: 8, md: 12 },
        backgroundColor: '#0d1216',
        position: 'relative',
        '&::before': {
          content: '""',
          position: 'absolute',
          top: '50%',
          left: '50%',
          transform: 'translate(-50%, -50%)',
          width: '80%',
          height: '80%',
          background: 'radial-gradient(ellipse, rgba(76, 175, 80, 0.05) 0%, transparent 70%)',
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
          Experiência Profissional
        </Typography>

        <Timeline position="alternate">
          {experiences.map((exp, index) => (
            <TimelineItem key={index}>
              <TimelineOppositeContent
                sx={{ m: 'auto 0' }}
                align={index % 2 === 0 ? 'right' : 'left'}
                variant="body2"
                color="text.secondary"
              >
                {exp.period}
              </TimelineOppositeContent>
              <TimelineSeparator>
                <TimelineConnector sx={{ bgcolor: index === 0 ? 'transparent' : 'primary.main' }} />
                <TimelineDot sx={{ bgcolor: 'primary.main', p: 1.5 }}>
                  <WorkIcon sx={{ fontSize: 28 }} />
                </TimelineDot>
                <TimelineConnector sx={{ bgcolor: index === experiences.length - 1 ? 'transparent' : 'primary.main' }} />
              </TimelineSeparator>
              <TimelineContent sx={{ py: '12px', px: 2 }}>
                <Paper
                  elevation={3}
                  sx={{
                    p: 3,
                    backgroundColor: 'background.default',
                    border: 1,
                    borderColor: 'divider',
                  }}
                >
                  <Typography variant="h5" component="h3" sx={{ color: 'primary.light', mb: 1 }}>
                    {exp.company}
                  </Typography>
                  <Typography variant="subtitle1" sx={{ color: 'text.secondary', mb: 2 }}>
                    {exp.role}
                  </Typography>
                  <Typography variant="body2" paragraph>
                    {exp.description}
                  </Typography>
                  <Stack direction="row" spacing={1} flexWrap="wrap" useFlexGap>
                    {exp.technologies.map((tech, idx) => (
                      <Chip
                        key={idx}
                        label={tech}
                        size="small"
                        sx={{
                          backgroundColor: 'secondary.main',
                          color: 'text.primary',
                          mb: 1,
                        }}
                      />
                    ))}
                  </Stack>
                </Paper>
              </TimelineContent>
            </TimelineItem>
          ))}
        </Timeline>
      </Container>
    </Box>
  );
};
