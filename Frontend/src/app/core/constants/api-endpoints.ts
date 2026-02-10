export const API_ENDPOINTS = {
  AUTH: {
    LOGIN: '/auth/login'
  },
  
  PRODUCTS: {
    BASE: '/product',
    BY_ID: (id: number) => `/product/${id}`,
    BY_CATEGORY: (categoryId: number) => `/product/category/${categoryId}`,
    UPDATE_PRICE: (id: number) => `/product/${id}`,
    DELETE: (id: number) => `/product/${id}`
  },
  
  CATEGORIES: {
    BASE: '/category',
    BY_ID: (id: number) => `/category/${id}`
  }
};