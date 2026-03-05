import { Box, Container, Typography, Stack, IconButton, Paper } from '@mui/material';
import {
  GitHub as GitHubIcon,
  LinkedIn as LinkedInIcon,
  Email as EmailIcon,
} from '@mui/icons-material';

export const ContactSection = () => {
  return (
    <Box
      id="contact"
      sx={{
        py: { xs: 8, md: 12 },
        backgroundColor: '#121820',
        position: 'relative',
      }}
    >
      <Container maxWidth="md">
        <Paper
          elevation={0}
          sx={{
            p: { xs: 4, md: 6 },
            textAlign: 'center',
            backgroundColor: 'background.paper',
            border: 2,
            borderColor: 'primary.main',
            borderRadius: 4,
          }}
        >
          <Typography
            variant="h2"
            gutterBottom
            sx={{ color: 'primary.main' }}
          >
            Entre em Contato
          </Typography>

          <Typography
            variant="h6"
            color="text.secondary"
            paragraph
            sx={{ mb: 4, maxWidth: 600, mx: 'auto' }}
          >
            Vamos trabalhar juntos? Me envie uma mensagem ou entre em contato
            pelas redes sociais.
          </Typography>

          <Stack
            direction="row"
            spacing={3}
            justifyContent="center"
            sx={{ mb: 3 }}
          >
            <IconButton
              component="a"
              href="https://github.com/mrffilipe"
              target="_blank"
              rel="noopener noreferrer"
              sx={{
                backgroundColor: 'primary.main',
                color: 'background.default',
                width: 50,
                height: 50,
                boxShadow: '0 4px 12px rgba(76, 175, 80, 0.3)',
                '&:hover': {
                  backgroundColor: 'primary.dark',
                  transform: 'translateY(-4px) scale(1.05)',
                  boxShadow: '0 8px 20px rgba(76, 175, 80, 0.4)',
                },
                transition: 'all 0.3s cubic-bezier(0.4, 0, 0.2, 1)',
              }}
            >
              <GitHubIcon sx={{ fontSize: 24 }} />
            </IconButton>

            <IconButton
              component="a"
              href="https://www.linkedin.com/in/mrffilipe/"
              target="_blank"
              rel="noopener noreferrer"
              sx={{
                backgroundColor: 'primary.main',
                color: 'background.default',
                width: 50,
                height: 50,
                boxShadow: '0 4px 12px rgba(76, 175, 80, 0.3)',
                '&:hover': {
                  backgroundColor: 'primary.dark',
                  transform: 'translateY(-4px) scale(1.05)',
                  boxShadow: '0 8px 20px rgba(76, 175, 80, 0.4)',
                },
                transition: 'all 0.3s cubic-bezier(0.4, 0, 0.2, 1)',
              }}
            >
              <LinkedInIcon sx={{ fontSize: 24 }} />
            </IconButton>

            <IconButton
              component="a"
              href="mailto:mrffilipe@gmail.com"
              sx={{
                backgroundColor: 'primary.main',
                color: 'background.default',
                width: 50,
                height: 50,
                boxShadow: '0 4px 12px rgba(76, 175, 80, 0.3)',
                '&:hover': {
                  backgroundColor: 'primary.dark',
                  transform: 'translateY(-4px) scale(1.05)',
                  boxShadow: '0 8px 20px rgba(76, 175, 80, 0.4)',
                },
                transition: 'all 0.3s cubic-bezier(0.4, 0, 0.2, 1)',
              }}
            >
              <EmailIcon sx={{ fontSize: 24 }} />
            </IconButton>
          </Stack>
        </Paper>
      </Container>
    </Box>
  );
};
