import { BrowserRouter, Routes, Route } from "react-router-dom"
import LoginPage from "./pages/LoginPage"
import HomePage from "./pages/HomePage"
import AdminPage from "./pages/AdminPage"
import ProtectedRoute from "./routes/ProtectedRoute"

export default function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/login" element={<LoginPage />} />

                <Route
                    path="/"
                    element={
                        <ProtectedRoute>
                            <HomePage />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/admin"
                    element={
                        <ProtectedRoute adminOnly>
                            <AdminPage />
                        </ProtectedRoute>
                    }
                />
            </Routes>
        </BrowserRouter>
    )
}