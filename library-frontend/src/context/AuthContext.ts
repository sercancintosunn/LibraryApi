import { createContext, useContext } from 'react'
import type { AuthResponse, Member } from '../api/types'

export interface AuthContextType {
    currentUser: Member | null
    signIn: (response: AuthResponse) => void
    signOut: () => void
}

export const AuthContext = createContext<AuthContextType | undefined>(undefined)

export function useAuth() {
    const context = useContext(AuthContext)
    if (!context) {
        throw new Error('useAuth, AuthProvider içinde kullanılmalı')
    }
    return context
}
