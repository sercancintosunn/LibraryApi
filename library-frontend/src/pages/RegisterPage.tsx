import React, { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { register } from '../api/authApi'

function RegisterPage() {

    const [fullName, setFullName] = useState('')
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [error, setError] = useState('')
    const navigate = useNavigate()

    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault()
        setError('')

        try {
            await register({ fullName, email, password })
            navigate("/login")
        } catch (err: any) {
            const message = err.response?.data?.message || "Kayıt olurken bir hata oluştu"
            setError(message)
        }
    }

    return (
        <div className="auth-page">
            <div className="auth-background">
                <div className="auth-shape auth-shape-one"></div>
                <div className="auth-shape auth-shape-two"></div>
            </div>

            <div className="auth-card register-card">
                <div className="auth-logo">
                    📚
                </div>

                <div className="auth-header">
                    <h1>Hesap Oluştur</h1>
                    <p>LibraryHub'a katıl ve kütüphaneni yönet</p>
                </div>

                <form onSubmit={handleSubmit} className="auth-form">

                    <div className="auth-field">
                        <label htmlFor="fullName">
                            Ad Soyad
                        </label>

                        <div className="input-wrapper">
                            <span className="input-icon">👤</span>

                            <input
                                id="fullName"
                                type="text"
                                placeholder="Sercan Çintosun"
                                value={fullName}
                                onChange={(e) =>
                                    setFullName(e.target.value)
                                }
                                required
                            />
                        </div>
                    </div>

                    <div className="auth-field">
                        <label htmlFor="register-email">
                            Email
                        </label>

                        <div className="input-wrapper">
                            <span className="input-icon">✉</span>

                            <input
                                id="register-email"
                                type="email"
                                placeholder="ornek@email.com"
                                value={email}
                                onChange={(e) =>
                                    setEmail(e.target.value)
                                }
                                required
                            />
                        </div>
                    </div>

                    <div className="auth-field">
                        <label htmlFor="register-password">
                            Şifre
                        </label>

                        <div className="input-wrapper">
                            <span className="input-icon">🔒</span>

                            <input
                                id="register-password"
                                type="password"
                                placeholder="••••••••"
                                value={password}
                                onChange={(e) =>
                                    setPassword(e.target.value)
                                }
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
                        Hesap Oluştur
                    </button>
                </form>

                <div className="auth-footer">
                    <span>Zaten hesabın var mı?</span>

                    <Link to="/login">
                        Giriş Yap
                    </Link>
                </div>
            </div>
        </div>
    )
}

export default RegisterPage