/** @type {import('next').NextConfig} */
const nextConfig = {
    reactStrictMode: true,
    webpack: (config, { dev }) => {
        if (dev) {
            config.devtool = 'eval-source-map';
        } else {
            config.devtool = false;
        }
        return config;
    },
};

export default nextConfig;
