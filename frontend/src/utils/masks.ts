export const phoneMask = (value: string): string => {
  const cleaned = value.replace(/\D/g, '');
  
  if (cleaned.length <= 10) {
    return cleaned.replace(/(\d{2})(\d{4})(\d{0,4})/, '($1) $2-$3');
  }
  
  return cleaned.replace(/(\d{2})(\d{5})(\d{0,4})/, '($1) $2-$3');
};

export const removeMask = (value: string): string => {
  return value.replace(/\D/g, '');
};

export const urlMask = (value: string): string => {
  if (!value) return '';
  
  if (!value.startsWith('http://') && !value.startsWith('https://')) {
    return value;
  }
  
  return value;
};

export const instagramMask = (value: string): string => {
  const cleaned = value.replace(/[^a-zA-Z0-9._]/g, '');
  
  if (cleaned.startsWith('@')) {
    return cleaned;
  }
  
  return cleaned ? `@${cleaned}` : '';
};
