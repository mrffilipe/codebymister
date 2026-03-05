import { useEffect } from 'react';
import { Box } from '@mui/material';
import { HeroSection } from './sections/HeroSection';
import { AboutSection } from './sections/AboutSection';
import { ProjectsSection } from './sections/ProjectsSection';
import { CertificatesSection } from './sections/CertificatesSection';
import { ExperienceSection } from './sections/ExperienceSection';
import { ContactSection } from './sections/ContactSection';
import { LandingHeader } from './components/LandingHeader';
import { LandingFooter } from './components/LandingFooter';

export const Landing = () => {
  useEffect(() => {
    // Componente montado - sem operações assíncronas pendentes
  }, []);

  return (
    <Box sx={{ minHeight: '100vh' }}>
      <LandingHeader />
      <HeroSection />
      <AboutSection />
      <ProjectsSection />
      <CertificatesSection />
      <ExperienceSection />
      <ContactSection />
      <LandingFooter />
    </Box>
  );
};
