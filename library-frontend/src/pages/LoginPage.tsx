import React, { useEffect, useState } from 'react'
import axios from 'axios'
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { login, warmApi } from '../api/authApi';
import { useAuth } from '../context/AuthContext';


function LoginPage() {


    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('')
    const [error, setError] = useState('')
    const [submitting, setSubmitting] = useState(false)
    const navigate = useNavigate()
    const location = useLocation()
    const { signIn } = useAuth()
    const registrationSucceeded = (location.state as { registered?: boolean } | null)?.registered

    useEffect(() => {
        warmApi()
    }, [])

    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault()
        setError('')
        setSubmitting(true)

        try {
            const result = await login({ email: email.trim(), password })
            signIn(result)
            navigate('/')
        } catch (err) {
            const status = axios.isAxiosError(err) ? err.response?.status : undefined
            setError(status === 401 || status === 403
                ? 'Email veya şifre hatalı'
                : 'Şu anda giriş yapılamıyor. Lütfen biraz sonra tekrar deneyin.')
        } finally {
            setSubmitting(false)
        }
    }

    return (
        <div className="auth-page">
            <div className="auth-background">
                <div className="auth-shape auth-shape-one"></div>
                <div className="auth-shape auth-shape-two"></div>
            </div>

            <div className="auth-card">
                <div className="auth-logo">
                    📚
                </div>

                <div className="auth-header">
                    <h1>Hoş Geldin</h1>
                    <p>LibraryHub hesabına giriş yap</p>
                </div>

                <form onSubmit={handleSubmit} className="auth-form">

                    {registrationSucceeded && (
                        <div className="auth-success" role="status">
                            Hesabın oluşturuldu. Şimdi giriş yapabilirsin.
                        </div>
                    )}

                    <div className="auth-field">
                        <label htmlFor="email">
                            Email
                        </label>

                        <div className="input-wrapper">
                            <span className="input-icon">✉</span>

                            <input
                                id="email"
                                type="email"
                                placeholder="ornek@email.com"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                required
                            />
                        </div>
                    </div>

                    <div className="auth-field">
                        <label htmlFor="password">
                            Şifre
                        </label>

                        <div className="input-wrapper">
                            <span className="input-icon">🔒</span>

                            <input
                                id="password"
                                type="password"
                                placeholder="••••••••"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                required
                            />
                        </div>
                    </div>

                    {error && (
                        <div className="auth-error" role="alert">
                            <span>!</span>
                            {error}
                        </div>
                    )}

                    <button
                        type="submit"
                        className="auth-submit"
                        disabled={submitting}
                    >
                        {submitting ? 'Giriş yapılıyor...' : 'Giriş Yap'}
                    </button>
                </form>

                <div className="auth-footer">
                    <span>Hesabın yok mu?</span>

                    <Link to="/register">
                        Kayıt Ol
                    </Link>
                </div>
            </div>
        </div>
    )
}

export default LoginPage
