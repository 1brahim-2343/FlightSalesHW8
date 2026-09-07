import { Routes, Route } from 'react-router-dom';
import Layout from './components/Layout';
import FlightsListPage from './pages/FlightsListPage';
import FlightDetailPage from './pages/FlightDetailPage';
import AddFlightPage from './pages/AddFlightPage';
import EditFlightPage from './pages/EditFlightPage';
import MyTicketsPage from './pages/MyTicketsPage';
import './App.css';

function App() {
    return (
        <Routes>
            <Route element={<Layout />}>
                <Route path="/" element={<FlightsListPage />} />
                <Route path="/flights/new" element={<AddFlightPage />} />
                <Route path="/flights/:id" element={<FlightDetailPage />} />
                <Route path="/flights/:id/edit" element={<EditFlightPage />} />
                <Route path="/my-tickets" element={<MyTicketsPage />} />
            </Route>
        </Routes>
    );
}

export default App;
