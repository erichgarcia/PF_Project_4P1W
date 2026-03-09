using auth_api.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

import { createContext, useContext, useMemo, useState } from "react"

type User = {
  email: string
  role: string
}

type AuthContextType = {
  token: string | null
  user: User | null
  login: (token: string, user: User) => void
  logout: () => void
}

const AuthContext = createContext < AuthContextType | undefined > (undefined)

export function AuthProvider({ children }: { children: React.ReactNode }) {
    const [token, setToken] = useState < string | null > (localStorage.getItem("token"))
  const [user, setUser] = useState < User | null > (() => {
      const saved = localStorage.getItem("user")
    return saved ? JSON.parse(saved) : null
  })

  const value = useMemo(
    () => ({
        token,
      user,
      login: (newToken: string, newUser: User) => {
            setToken(newToken)
        setUser(newUser)
        localStorage.setItem("token", newToken)
        localStorage.setItem("user", JSON.stringify(newUser))
      },
      logout: () => {
          setToken(null)
        setUser(null)
        localStorage.removeItem("token")
        localStorage.removeItem("user")
      }
    }),
    [token, user]
  )

  return < AuthContext.Provider value ={ value}>{ children}</ AuthContext.Provider >
}

export function useAuth() {
  const context = useContext(AuthContext)
  if (!context) throw new Error("useAuth must be used inside AuthProvider")
  return context
}