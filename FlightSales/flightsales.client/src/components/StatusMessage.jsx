const StatusMessage = ({ tone = 'info', title, children }) => (
    <div className={`status-message status-message--${tone}`}>
        {title && <p className="status-message-title">{title}</p>}
        {children && <p className="status-message-body">{children}</p>}
    </div>
);

export default StatusMessage;
