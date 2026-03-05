import { Box, Container, Typography, Card, CardContent, Button } from '@mui/material';
import { Verified as VerifiedIcon } from '@mui/icons-material';

const certificates = [
  {
    id: 1,
    title: 'C# COMPLETO Programação Orientada a Objetos + Projetos',
    issuer: 'Udemy',
    date: 'Outubro de 2022',
    link: 'https://www.udemy.com/certificate/UC-0235e5d5-ca64-41e5-b3a5-44d62b07c068/',
  },
  {
    id: 2,
    title: 'React do Zero a Maestria (c/ hooks, router, API, Projetos)',
    issuer: 'Udemy',
    date: 'Outubro de 2024',
    link: 'https://www.udemy.com/certificate/UC-0f174565-0d23-42fe-a589-61abdc1d2235/',
  },
  {
    id: 3,
    title: 'Rock .NET 5 Entity Framework',
    issuer: 'Udemy',
    date: 'Outubro de 2022',
    link: 'https://www.udemy.com/certificate/UC-3f3a78a0-c003-4fe4-8b2a-b5959a5cffd0/',
  },
  {
    id: 4,
    title: 'Asp.Net Core 6 Web API, Segurança com JWT e MS Identity Core',
    issuer: 'Udemy',
    date: 'Outubro de 2023',
    link: 'https://www.udemy.com/certificate/UC-291bb720-92de-49ad-8c5e-5e7b932438fb/',
  },
  {
    id: 5,
    title: 'Desenvolvimento de Aplicativos Android usando Kotlin',
    issuer: 'Udemy',
    date: 'Outubro de 2024',
    link: 'https://www.udemy.com/certificate/UC-5bafa045-e5bf-4739-a13f-ca5b5fc31e4e/',
  },
  {
    id: 6,
    title: 'Aprenda tudo sobre o Linux! Completo e atualizado v2024!',
    issuer: 'Udemy',
    date: 'Outubro de 2024',
    link: 'https://www.udemy.com/certificate/UC-6627ad18-dd03-45f8-aa9f-9506be40ded8/',
  },
];

export const CertificatesSection = () => {
  return (
    <Box
      id="certificates"
      sx={{
        py: { xs: 8, md: 12 },
        backgroundColor: '#161d23',
        position: 'relative',
        '&::after': {
          content: '""',
          position: 'absolute',
          bottom: 0,
          right: 0,
          width: '60%',
          height: '60%',
          background: 'radial-gradient(circle at bottom right, rgba(76, 175, 80, 0.04) 0%, transparent 70%)',
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
          Certificados
        </Typography>

        <Box
          sx={{
            display: 'grid',
            gridTemplateColumns: {
              xs: '1fr',
              sm: 'repeat(2, 1fr)',
              md: 'repeat(3, 1fr)',
            },
            gap: 3,
          }}
        >
          {certificates.map((cert) => (
            <Box key={cert.id}>
              <Card
                sx={{
                  height: '100%',
                  display: 'flex',
                  flexDirection: 'column',
                  transition: 'all 0.3s cubic-bezier(0.4, 0, 0.2, 1)',
                  boxShadow: '0 4px 12px rgba(0,0,0,0.3)',
                  '&:hover': {
                    transform: 'translateY(-8px)',
                    boxShadow: '0 12px 24px rgba(76, 175, 80, 0.25)',
                  },
                }}
              >
                <CardContent sx={{ flexGrow: 1, display: 'flex', flexDirection: 'column' }}>
                  <Box sx={{ display: 'flex', alignItems: 'flex-start', mb: 2 }}>
                    <VerifiedIcon sx={{ color: 'primary.main', mr: 1, mt: 0.5, fontSize: 28 }} />
                    <Typography variant="h6" sx={{ flexGrow: 1, lineHeight: 1.3 }}>
                      {cert.title}
                    </Typography>
                  </Box>

                  <Typography variant="body2" color="text.secondary" gutterBottom>
                    {cert.issuer}
                  </Typography>

                  <Typography variant="caption" color="text.secondary" sx={{ mb: 2 }}>
                    {cert.date}
                  </Typography>

                  <Button
                    variant="outlined"
                    size="small"
                    href={cert.link}
                    target="_blank"
                    rel="noopener noreferrer"
                    sx={{ mt: 'auto' }}
                  >
                    Ver Certificado
                  </Button>
                </CardContent>
              </Card>
            </Box>
          ))}
        </Box>
      </Container>
    </Box>
  );
};
