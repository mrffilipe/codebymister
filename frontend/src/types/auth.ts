export interface AuthSession {
  accessToken: string;
  expiresAt: string;
  userId: string;
  externalAuthId: string;
  email: string;
}
