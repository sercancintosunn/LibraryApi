import React, { useEffect, useState } from 'react'
import axios from 'axios'
import { Link, useNavigate } from 'react-router-dom'
import { register, warmApi } from '../api/authApi'

function RegisterPage() {

    const [fullName, setFullName] = useState('')
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [error, setError] = useState('')
    const [submitting, setSubmitting] = useState(false)
    const navigate = useNavigate()

    useEffect(() => {
        warmApi()
    }, [])

    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault()
        setError('')
        setSubmitting(true)

        try {
            await register({ fullName: fullName.trim(), email: email.trim(), password })
            navigate('/login', { state: { registered: true } })
        } catch (err) {
            const status = axios.isAxiosError(err) ? err.response?.status : undefined
            setError(status === 409
                ? 'Bu email ile zaten biri kayıtlı'
                : status === 400
                    ? 'Kayıt bilgilerini kontrol edin.'
                    : 'Şu anda kayıt olunamıyor. Lütfen biraz sonra tekrar deneyin.')
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
                        {submitting ? 'Hesap oluşturuluyor...' : 'Hesap Oluştur'}
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
