ECHO off

sqlcmd -S localhost\SQLEXPRESS -E -i SQL/Night_Rider.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_accommodation.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_bid.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_change_order.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_change_order_line.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_charter.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_charter_accommodation.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_charter_assignment.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_charter_rider.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_charter_stop.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_client.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_client_accommodation.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_client_client_role.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_client_credential.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_client_dependent_role.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_client_role.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_dependent.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_dependent_accommodation.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_driver.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_driver_license_class.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_driver_unavailable.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_employee.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_employee_role.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_inspection_report.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_login.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_maintenance_schedule.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_model_compatibility.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_model_lookup.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_notification.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_packing_slip.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_parts_inventory.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_parts_request.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_password_reset.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_purchase_order.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_purchase_order_line_items.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_refuel_log.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_rental_vehicle.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_ride.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_role.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_route.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_route_assignment.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_route_stop.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_safety_report.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_schedule.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_service.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_service_assignment.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_service_detail.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_service_line.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_service_line_item.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_service_order.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_service_type.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_source.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_special_inspection.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_special_service_order.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_special_work_order.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_stop.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_support_ticket.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_support_ticket_employee_line.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_ticket_type.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_vehicle.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_vehicle_checklist.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_vehicle_driver.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_vehicle_type.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_vehicle_unavailable.sql
sqlcmd -S localhost\SQLEXPRESS -E -i SQL/sp_vendor.sql

rem server is local host

ECHO .
ECHO if no errors appear DB was created
PAUSE

