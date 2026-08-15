import React, { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom';
import { login } from '../api/authApi';
import { saveToken } from '../api/authStorage';
import { useAuth } from '../context/AuthContext';


function LoginPage() {


    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('')
    const [error, setError] = useState('')
    const navigate = useNavigate()
    const { refreshUser } = useAuth()

    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault()
        setError('')

        try {
            const result = await login({ email, password })
            saveToken(result.token)
            await refreshUser()
            navigate('/')
        } catch (err) {
            setError('Email veya şifre hatalı')
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
                        <div className="auth-error">
                            <span>!</span>
                            {error}
                        </div>
                    )}

                    <button
                        type="submit"
                        className="auth-submit"
                    >
                        Giriş Yap
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
