import { BrowserRouter, Routes, Route, Navigate } from 'react-router';
import { ThemeProvider } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import { AuthProvider } from './contexts/AuthContext';
import { PrivateRoute } from './components/Layout/PrivateRoute';
import { MainLayout } from './components/Layout/MainLayout';
import { Login } from './pages/Login';
import { Dashboard } from './pages/Dashboard';
import { Leads } from './pages/Leads';
import { Outreaches } from './pages/Outreaches';
import { Conversations } from './pages/Conversations';
import { Proposals } from './pages/Proposals';
import { Projects } from './pages/Projects';
import { Maintenances } from './pages/Maintenances';
import { theme } from './theme';

function App() {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <AuthProvider>
        <BrowserRouter>
          <Routes>
            <Route path="/login" element={<Login />} />
            <Route
              path="/"
              element={
                <PrivateRoute>
                  <MainLayout>
                    <Dashboard />
                  </MainLayout>
                </PrivateRoute>
              }
            />
            <Route
              path="/leads"
              element={
                <PrivateRoute>
                  <MainLayout>
                    <Leads />
                  </MainLayout>
                </PrivateRoute>
              }
            />
            <Route
              path="/outreach"
              element={
                <PrivateRoute>
                  <MainLayout>
                    <Outreaches />
                  </MainLayout>
                </PrivateRoute>
              }
            />
            <Route
              path="/conversations"
              element={
                <PrivateRoute>
                  <MainLayout>
                    <Conversations />
                  </MainLayout>
                </PrivateRoute>
              }
            />
            <Route
              path="/proposals"
              element={
                <PrivateRoute>
                  <MainLayout>
                    <Proposals />
                  </MainLayout>
                </PrivateRoute>
              }
            />
            <Route
              path="/projects"
              element={
                <PrivateRoute>
                  <MainLayout>
                    <Projects />
                  </MainLayout>
                </PrivateRoute>
              }
            />
            <Route
              path="/maintenance"
              element={
                <PrivateRoute>
                  <MainLayout>
                    <Maintenances />
                  </MainLayout>
                </PrivateRoute>
              }
            />
            <Route path="*" element={<Navigate to="/" replace />} />
          </Routes>
        </BrowserRouter>
      </AuthProvider>
    </ThemeProvider>
  );
}

export default App;
