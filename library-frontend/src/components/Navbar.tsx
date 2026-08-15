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
        <nav className="main-navbar">
            <Link to="/" className="navbar-brand">
                <span className="navbar-logo">📚</span>
                LibraryHub
            </Link>

            <div className="navbar-links">
                <Link to="/" className="nav-link">
                    Kitaplar
                </Link>

                <Link to="/add-book" className="nav-link">
                    Kitap Ekle
                </Link>

                <Link to="/loans" className="nav-link">
                    Ödünç Kayıtları
                </Link>

                <Link to="/authors" className="nav-link">
                    Yazarlar
                </Link>

                <Link to="/categories" className="nav-link">
                    Kategoriler
                </Link>
            </div>

            <div className="navbar-user">
                {currentUser ? (
                    <>
                        <span className="user-name">
                            Hoş geldin, {currentUser.fullName}
                        </span>

                        <button
                            className="logout-button"
                            onClick={handleLogout}
                        >
                            Çıkış Yap
                        </button>
                    </>
                ) : (
                    <Link to="/login" className="nav-link">
                        Giriş Yap
                    </Link>
                )}
            </div>
        </nav>
    )
}

export default Navbar