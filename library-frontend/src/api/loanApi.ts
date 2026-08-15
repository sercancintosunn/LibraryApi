import axiosClient from "./axiosClient";
import type { Loan, CreateLoanRequest } from './types'


export async function getAllLoans(): Promise<Loan[]> {
    const response = await axiosClient.get<Loan[]>('/Loans')
    return response.data
}

export async function createLoan(data: CreateLoanRequest): Promise<void> {
    await axiosClient.post('/Loans', data)
}

export async function returnLoan(loanId: number): Promise<void> {
    await axiosClient.put(`/Loans/${loanId}/return`)

}