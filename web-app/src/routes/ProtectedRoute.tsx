import { Navigate } from "react-router-dom"
import { useAuth } from "../context/AuthContext"

type Props = {
    children: React.ReactNode
    adminOnly?: boolean
}

export default function ProtectedRoute({ children, adminOnly = false }: Props) {
    const { token, user } = useAuth()

    if (!token) {
        return <Navigate to="/login" replace />
    }

    if (adminOnly && user?.role !== "admin") {
        return <Navigate to="/login" replace />
    }

    return <>{children}</>
}