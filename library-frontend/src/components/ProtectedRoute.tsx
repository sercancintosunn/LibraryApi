import { Navigate } from 'react-router-dom'
import { getToken } from '../api/authStorage'

interface ProtectedRouteProps {
    children: React.ReactNode
}


function ProtectedRoute({ children }: ProtectedRouteProps) {
    const isLoggedIn = !!getToken()

    if (!isLoggedIn) {
        return <Navigate to="/login" replace />
    }

    return <>{children}</>
}


export default ProtectedRoute