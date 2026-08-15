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
        <div className='container'>
            <h1>Giriş Yap</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Email</label>
                    <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
                </div>
                <div>
                    <label >Şifre</label>
                    <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
                </div>
                {error && <p style={{ color: 'red' }}>{error}</p>}
                <button type='submit'>Giriş Yap</button>
                <p style={{ marginTop: '1rem', fontSize: '0.85rem' }}>
                    Hesabın yok mu? <Link to="/register">Kayıt Ol</Link>
                </p>
            </form>
        </div>
    )
}

export default LoginPage
