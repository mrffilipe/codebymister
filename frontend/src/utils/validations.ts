import * as yup from 'yup';

export const leadValidationSchema = yup.object({
  name: yup.string().required('Nome é obrigatório'),
  segment: yup.string().required('Segmento é obrigatório'),
  city: yup.string().required('Cidade é obrigatória'),
  problemDescription: yup.string().required('Descrição do problema é obrigatória'),
  priority: yup.number().required('Prioridade é obrigatória'),
  source: yup.number().required('Origem é obrigatória'),
  website: yup
    .string()
    .url('URL inválida')
    .nullable()
    .transform((value) => (value === '' ? null : value)),
  instagram: yup.string().nullable(),
  phone: yup
    .string()
    .nullable()
    .test('phone', 'Telefone inválido', (value) => {
      if (!value) return true;
      const cleaned = value.replace(/\D/g, '');
      return cleaned.length === 10 || cleaned.length === 11;
    }),
});

export const outreachValidationSchema = yup.object({
  leadId: yup.string().required('Lead é obrigatório'),
  channel: yup.number().required('Canal é obrigatório'),
  message: yup.string().required('Mensagem é obrigatória'),
  scheduledDate: yup.date().required('Data é obrigatória'),
});

export const proposalValidationSchema = yup.object({
  leadId: yup.string().required('Lead é obrigatório'),
  title: yup.string().required('Título é obrigatório'),
  description: yup.string().required('Descrição é obrigatória'),
  value: yup.number().positive('Valor deve ser positivo').required('Valor é obrigatório'),
  deadline: yup.date().required('Prazo é obrigatório'),
});

export const projectValidationSchema = yup.object({
  proposalId: yup.string().required('Proposta é obrigatória'),
  startDate: yup.date().required('Data de início é obrigatória'),
  estimatedEndDate: yup.date().required('Data estimada de término é obrigatória'),
  actualValue: yup.number().positive('Valor deve ser positivo').required('Valor é obrigatório'),
});

export const maintenanceValidationSchema = yup.object({
  projectId: yup.string().required('Projeto é obrigatório'),
  description: yup.string().required('Descrição é obrigatória'),
  monthlyValue: yup.number().positive('Valor deve ser positivo').required('Valor mensal é obrigatório'),
  startDate: yup.date().required('Data de início é obrigatória'),
});

export const conversationValidationSchema = yup.object({
  leadId: yup.string().required('Lead é obrigatório'),
  notes: yup.string().required('Notas são obrigatórias'),
  nextSteps: yup.string().nullable(),
});
