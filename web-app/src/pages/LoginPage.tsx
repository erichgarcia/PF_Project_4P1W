import { useState } from "react"
import { useNavigate } from "react-router-dom"
import { useAuth } from "../context/AuthContext"

export default function LoginPage() {
    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")
    const { login } = useAuth()
    const navigate = useNavigate()

    const handleLogin = async () => {
        const res = await fetch("http://localhost:5000/auth/login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ email, password })
        })

        const data = await res.json()

        if (!res.ok) {
            alert(data.message || "Login failed")
            return
        }

        login(data.token, {
            email: data.email,
            role: data.role
        })

        navigate("/")
    }

    return (
        <div style={{ display: "flex", flexDirection: "column", gap: "10px", width: "240px", margin: "80px auto" }}>
            <h2>Login</h2>

            <input
                placeholder="Email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
            />

            <input
                type="password"
                placeholder="Password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
            />

            <button onClick={handleLogin}>Login</button>
        </div>
    )
}