import { createContext, useContext, useEffect, useState, type ReactNode } from 'react';
import {
  signInWithEmailAndPassword,
  signOut,
  onAuthStateChanged,
} from 'firebase/auth';
import type { User } from 'firebase/auth';
import { auth } from '../config/firebase';
import { authService } from '../services/authService';
import type { AuthSession } from '../types/auth';
import { clearAuthSession, getAuthSession, setAuthSession } from '../utils/authStorage';

interface AuthContextType {
  user: User | null;
  session: AuthSession | null;
  isAuthenticated: boolean;
  loading: boolean;
  login: (email: string, password: string) => Promise<void>;
  logout: () => Promise<void>;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: ReactNode }) {
  const [user, setUser] = useState<User | null>(null);
  const [session, setSessionState] = useState<AuthSession | null>(() => getAuthSession());
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const unsubscribe = onAuthStateChanged(auth, async (firebaseUser: User | null) => {
      setUser(firebaseUser);

      if (!firebaseUser) {
        clearAuthSession();
        setSessionState(null);
        setLoading(false);
        return;
      }

      const existingSession = getAuthSession();
      if (existingSession) {
        setSessionState(existingSession);
        setLoading(false);
        return;
      }

      try {
        const firebaseIdToken = await firebaseUser.getIdToken();
        const newSession = await authService.exchangeToken(firebaseIdToken);
        setAuthSession(newSession);
        setSessionState(newSession);
      } catch {
        clearAuthSession();
        setSessionState(null);
      } finally {
        setLoading(false);
      }
    });

    return unsubscribe;
  }, []);

  const login = async (email: string, password: string) => {
    const credential = await signInWithEmailAndPassword(auth, email, password);
    const firebaseIdToken = await credential.user.getIdToken();

    const newSession = await authService.exchangeToken(firebaseIdToken);
    setAuthSession(newSession);
    setSessionState(newSession);
  };

  const logout = async () => {
    clearAuthSession();
    setSessionState(null);
    await signOut(auth);
  };

  const isAuthenticated = Boolean(user && session?.accessToken);

  return (
    <AuthContext.Provider value={{ user, session, isAuthenticated, loading, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
}
