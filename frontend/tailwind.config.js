module.exports = {
  theme: {
    extend: {
        fontFamily: {
        montserratAlt: ['var(--font-montserrat-alternates)', 'sans-serif'],
        montserrat: ['var(--font-montserrat)', 'sans-serif'],
      },
      keyframes: {
        fadeIn: {
          '0%': { opacity: '0' },
          '100%': { opacity: '1' },
        }
      },
      animation: {
        fadeIn: 'fadeIn 0.3s forwards',
      }
    }
  }
}
