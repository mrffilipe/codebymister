import { useState } from 'react';
import type { ReactNode } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  Stepper,
  StepLabel,
  Box,
  useMediaQuery,
  useTheme,
} from '@mui/material';

interface StepperDialogProps {
  open: boolean;
  title: string;
  steps: string[];
  children: ReactNode[];
  onClose: () => void;
  onSave: () => void | Promise<void>;
  maxWidth?: 'xs' | 'sm' | 'md' | 'lg' | 'xl';
}

export function StepperDialog({
  open,
  title,
  steps,
  children,
  onClose,
  onSave,
  maxWidth = 'xs',
}: StepperDialogProps) {
  const [activeStep, setActiveStep] = useState(0);
  const [loading, setLoading] = useState(false);
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('sm'));

  const handleNext = () => {
    setActiveStep((prevStep) => prevStep + 1);
  };

  const handleBack = () => {
    setActiveStep((prevStep) => prevStep - 1);
  };

  const handleClose = () => {
    setActiveStep(0);
    onClose();
  };

  const handleSave = async () => {
    setLoading(true);
    try {
      await onSave();
      setActiveStep(0);
    } catch (error) {
      console.error('Erro ao salvar:', error);
    } finally {
      setLoading(false);
    }
  };

  const isLastStep = activeStep === steps.length - 1;

  return (
    <Dialog
      open={open}
      onClose={handleClose}
      maxWidth={maxWidth}
      fullWidth
      fullScreen={isMobile}
    >
      <DialogTitle sx={{ pb: 2 }}>{title}</DialogTitle>
      <DialogContent sx={{ px: { xs: 2, sm: 3 } }}>
        <Box sx={{ width: '100%', mt: 3 }}>
          <Stepper activeStep={activeStep} alternativeLabel={!isMobile}>
            {steps.map((label) => (
              <StepLabel key={label}>{label}</StepLabel>
            ))}
          </Stepper>
          <Box sx={{ mt: 4, mb: 2 }}>{children[activeStep]}</Box>
        </Box>
      </DialogContent>
      <DialogActions sx={{ px: 3, pb: 3, pt: 2 }}>
        <Button onClick={handleClose} disabled={loading}>
          Cancelar
        </Button>
        <Box sx={{ flex: '1 1 auto' }} />
        <Button
          disabled={activeStep === 0 || loading}
          onClick={handleBack}
          sx={{ mr: 1 }}
        >
          Voltar
        </Button>
        {isLastStep ? (
          <Button onClick={handleSave} variant="contained" disabled={loading}>
            {loading ? 'Salvando...' : 'Salvar'}
          </Button>
        ) : (
          <Button onClick={handleNext} variant="contained">
            Avançar
          </Button>
        )}
      </DialogActions>
    </Dialog>
  );
}
