import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.tsx'
import { BrowserRouter } from 'react-router'
import "./components/Auth/SetupFetch.ts";

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <BrowserRouter>        
          <App/>
      </BrowserRouter>
  </StrictMode>,
)
