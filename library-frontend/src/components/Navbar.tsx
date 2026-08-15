import { Link, useNavigate } from "react-router-dom";
import { removeToken } from "../api/authStorage";
import { useAuth } from "../context/AuthContext";

function Navbar() {

    const navigate = useNavigate()
    const { currentUser, refreshUser } = useAuth()

    async function handleLogout() {
        removeToken()
        await refreshUser()
        navigate('/login')
    }

    return (
        <nav style={{ display: 'flex', gap: '1rem', padding: '1rem', borderBottom: '1px solid #ccc' }}>
            <Link to="/">Kitaplar</Link>
            <Link to="/add-book">Kitap Ekle</Link>
            <Link to="/loans">Ödünç Kayıtları</Link>
            <Link to="/authors">Yazarlar</Link>
            <Link to="/categories">Kategoriler</Link>
            {currentUser ? (
                <>
                    <span>Hoşgeldin,{currentUser.fullName}</span>
                    <button onClick={handleLogout}>Çıkış Yap</button>
                </>
            ) : (
                <Link to="/login">Giriş Yap</Link>
            )

            }
        </nav>
    )
}

export default Navbar