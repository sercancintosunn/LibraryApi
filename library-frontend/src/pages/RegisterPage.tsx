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
        <div className='login-wrapper'>
            <form onSubmit={handleSubmit} className='login-form'>
                <h1>Kütüphane Sistemi</h1>
                <p className='subtitle'>Yeni hesap oluştur</p>
                <div>
                    <label>Ad Soyad</label>
                    <input value={fullName} onChange={(e) => setFullName(e.target.value)} />
                </div>
                <div>
                    <label htmlFor="">Email</label>
                    <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
                </div>
                <div>
                    <label htmlFor="">Şifre</label>
                    <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
                </div>
                {error && <p style={{ color: 'red' }}>{error}</p>}
                <button type='submit'>Kayıt Ol</button>
                <p style={{ marginTop: '1rem', fontSize: '0.85rem' }}>Zaten hesabın var mı? <Link to={"/login"}>Giriş Yap</Link></p>
            </form>
        </div>
    )
}

export default RegisterPage