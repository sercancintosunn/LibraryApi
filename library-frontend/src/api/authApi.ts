import axiosClient from "./axiosClient";
import type { LoginRequest, AuthResponse, RegisterRequest } from "./types";

export async function login(data: LoginRequest): Promise<AuthResponse> {
    const response = await axiosClient.post<AuthResponse>('/Members/login', data)
    return response.data

}


export async function register(data: RegisterRequest): Promise<AuthResponse> {
    const response = await axiosClient.post<AuthResponse>("/Members/register", data)
    return response.data


}