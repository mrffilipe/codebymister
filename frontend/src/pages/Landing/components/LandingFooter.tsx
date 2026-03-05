import { Box, Container, Typography, IconButton, Stack } from '@mui/material';
import {
  GitHub as GitHubIcon,
  LinkedIn as LinkedInIcon,
  Email as EmailIcon,
} from '@mui/icons-material';

export const LandingFooter = () => {
  return (
    <Box
      component="footer"
      sx={{
        py: 6,
        backgroundColor: 'background.paper',
        borderTop: 1,
        borderColor: 'divider',
      }}
    >
      <Container maxWidth="lg">
        <Stack spacing={1.5} alignItems="center">
          <Box
            component="img"
            src="/logo.svg"
            alt="Code by Mister"
            sx={{
              height: 72,
              width: 'auto',
            }}
          />

          <Stack direction="row" spacing={2}>
            <IconButton
              component="a"
              href="https://github.com/mrffilipe"
              target="_blank"
              rel="noopener noreferrer"
              sx={{
                color: 'primary.light',
                '&:hover': { color: 'primary.main', transform: 'scale(1.1)' },
                transition: 'all 0.3s ease',
              }}
            >
              <GitHubIcon sx={{ fontSize: 28 }} />
            </IconButton>
            <IconButton
              component="a"
              href="https://www.linkedin.com/in/mrffilipe/"
              target="_blank"
              rel="noopener noreferrer"
              sx={{
                color: 'primary.light',
                '&:hover': { color: 'primary.main', transform: 'scale(1.1)' },
                transition: 'all 0.3s ease',
              }}
            >
              <LinkedInIcon sx={{ fontSize: 28 }} />
            </IconButton>
            <IconButton
              component="a"
              href="mailto:mrffilipe@gmail.com"
              sx={{
                color: 'primary.light',
                '&:hover': { color: 'primary.main', transform: 'scale(1.1)' },
                transition: 'all 0.3s ease',
              }}
            >
              <EmailIcon sx={{ fontSize: 28 }} />
            </IconButton>
          </Stack>

          <Typography
            component="a"
            href="/login"
            variant="caption"
            sx={{
              color: 'text.disabled',
              textDecoration: 'none',
              fontSize: '0.7rem',
              opacity: 0.4,
              '&:hover': {
                color: 'primary.main',
                opacity: 1,
              },
              transition: 'all 0.3s ease',
            }}
          >
            ⚙ Dashboard
          </Typography>

          <Typography variant="body2" color="text.secondary" sx={{ fontSize: '0.85rem' }}>
            © {new Date().getFullYear()} Code by Mister. Todos os direitos reservados.
          </Typography>
        </Stack>
      </Container>
    </Box>
  );
};
