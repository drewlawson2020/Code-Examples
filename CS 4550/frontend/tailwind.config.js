/** @type {import('tailwindcss').Config} */
export default {
  content: [
  "./index.html",
  "./src/**/*.{js,jsx}",
],
  theme: {
    extend: {
      height:{
        messages: "calc(100vh - 44px)"
      }
    },
  },
  plugins: [],
}

