import { NavLink, Outlet } from 'react-router-dom';
import './Layout.css';

const Layout = () => (
    <div className="app-shell">
        <header className="app-header">
            <NavLink to="/" className="app-brand">
                <span className="app-brand-mark">✈</span>
                FlightSales
            </NavLink>
            <nav className="app-nav">
                <NavLink to="/" end>Flights</NavLink>
                <NavLink to="/flights/new">Add flight</NavLink>
                <NavLink to="/my-tickets">My tickets</NavLink>
            </nav>
        </header>
        <main className="app-main">
            <Outlet />
        </main>
    </div>
);

export default Layout;
