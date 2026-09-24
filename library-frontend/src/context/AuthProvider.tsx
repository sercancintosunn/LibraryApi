import { useEffect, useState } from 'react'
import type { ReactNode } from 'react'
import { getMe } from '../api/memberApi'
import { getToken, removeToken, saveToken } from '../api/authStorage'
import type { AuthResponse, Member } from '../api/types'
import { AuthContext } from './AuthContext'

export function AuthProvider({ children }: { children: ReactNode }) {
    const [currentUser, setCurrentUser] = useState<Member | null>(null)

    useEffect(() => {
        const token = getToken()
        if (!token) return

        let active = true
        getMe()
            .then(member => {
                if (active && getToken() === token) setCurrentUser(member)
            })
            .catch(() => {
                if (active && getToken() === token) setCurrentUser(null)
            })

        return () => { active = false }
    }, [])

    function signIn(response: AuthResponse) {
        saveToken(response.token)
        setCurrentUser(response.member)
    }

    function signOut() {
        removeToken()
        setCurrentUser(null)
    }

    return (
        <AuthContext.Provider value={{ currentUser, signIn, signOut }}>
            {children}
        </AuthContext.Provider>
    )
}
