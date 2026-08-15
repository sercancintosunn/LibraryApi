import { createContext, useContext, useState, useEffect } from 'react'
import type { ReactNode } from 'react'
import { getMe } from '../api/memberApi'
import { getToken } from '../api/authStorage'
import type { Member } from '../api/types'

interface AuthContextType {
    currentUser: Member | null,
    loading: boolean,
    refreshUser: () => Promise<void>
}

const AuthContext = createContext<AuthContextType | undefined>(undefined)


export function AuthProvider({ children }: { children: ReactNode }) {
    const [currentUser, setCurrentUser] = useState<Member | null>(null)
    const [loading, setLoading] = useState(true)

    async function refreshUser() {
        const token = getToken()
        if (!token) {
            setCurrentUser(null)
            setLoading(false)
            return
        }

        try {
            const me = await getMe()
            setCurrentUser(me)
        } catch {
            setCurrentUser(null)
        } finally {
            setLoading(false)
        }
    }

    useEffect(() => {
        refreshUser()
    }, [])

    return (
        <AuthContext.Provider value={{ currentUser, loading, refreshUser }}>
            {children}
        </AuthContext.Provider>
    )
}

export function useAuth() {
    const context = useContext(AuthContext)
    if (!context) {
        throw new Error('useAuth,AuthProvider içinde kullanılmalı')
    }
    return context
}