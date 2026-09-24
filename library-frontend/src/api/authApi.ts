import axiosClient from "./axiosClient";
import type { LoginRequest, AuthResponse, RegisterRequest, Member } from "./types";

export function warmApi(): void {
    const apiUrl = import.meta.env.VITE_API_URL
    if (!apiUrl) return

    void fetch(new URL('/live', apiUrl)).catch(() => {
        // Login and registration show their own request errors.
    })
}

export async function login(data: LoginRequest): Promise<AuthResponse> {
    const response = await axiosClient.post<AuthResponse>('/Members/login', data)
    return response.data

}


export async function register(data: RegisterRequest): Promise<Member> {
    const response = await axiosClient.post<Member>("/Members/register", data)
    return response.data


}
