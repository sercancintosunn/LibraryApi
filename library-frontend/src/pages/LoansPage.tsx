import { useState, useEffect } from 'react'
import { getAllLoans, returnLoan } from '../api/loanApi'
import type { Loan } from '../api/types'

function LoansPage() {
    const [loans, setLoans] = useState<Loan[]>([])

    useEffect(() => {
        loadLoans()
    }, [])

    function loadLoans() {
        getAllLoans().then(setLoans)
    }

    async function handleReturn(loanId: number) {
        try {
            await returnLoan(loanId)
            loadLoans()
        } catch (err) {
            alert('İade işlemi başarısız.Bu kaydı iade etme yetkiniz olmayabilir')
        }

    }



    return (
        <div className='container'>
            <h1>Ödünç Kayıtları</h1>
            <table>
                <thead>
                    <tr>
                        <th>Kitap</th>
                        <th>Üye</th>
                        <th>Ödünç Tarihi</th>
                        <th>İade Tarihi</th>
                        <th></th>

                    </tr>
                </thead>
            </table>
            <tbody>
                {loans.map((loan) => (
                    <tr key={loan.id}>
                        <td>{loan.bookTitle}</td>
                        <td>{loan.memberName}</td>
                        <td>{new Date(loan.loanDate).toLocaleDateString('tr-TR')}</td>
                        <td>
                            {loan.returnDate
                                ? new Date(loan.returnDate).toLocaleDateString('tr-TR')
                                : 'İade edilmedi'}
                        </td>
                        <td>
                            {!loan.returnDate && (
                                <button onClick={() => handleReturn(loan.id)}>İade Et</button>
                            )}
                        </td>
                    </tr>
                ))}
            </tbody>
        </div>
    )
}

export default LoansPage