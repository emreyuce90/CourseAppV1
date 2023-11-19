import axios from 'axios';
import { getToken } from './TokenManager';


const instance = axios.create();

// Axios'un request interceptor'ı
instance.interceptors.request.use(
  (config) => {
    // İsteği göndermeden önce token ekleyin
    const token = getToken();
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Axios'un error interceptor'ı
instance.interceptors.response.use(
  (response) => response,
  (error) => {
    // Eğer 401 hatası alınırsa ve kullanıcı oturumu geçersizse
    if (error.response.status === 401) {
      // Kullanıcıyı login sayfasına yönlendir
      // Bu kısmı react-router veya başka bir yönlendirme mekanizmasıyla değiştirebilirsiniz
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

export default instance;
