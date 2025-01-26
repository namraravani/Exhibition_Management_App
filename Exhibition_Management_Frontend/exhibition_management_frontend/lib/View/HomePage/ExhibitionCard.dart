import 'package:flutter/material.dart';
import 'package:exhibition_management_frontend/Models/ExhibitionCardDTO.dart';

class ExhibitionDisplayCard extends StatelessWidget {
  final ExhibitionCardDTO model;

  const ExhibitionDisplayCard({Key? key, required this.model})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Card(
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(12.0),
      ),
      elevation: 4,
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          // Image and Event Status section using Stack
          Stack(
            children: [
              ClipRRect(
                borderRadius: BorderRadius.vertical(top: Radius.circular(12.0)),
                child: Image.asset(
                  model.imageUrl,
                  height: 300,
                  width: double.infinity,
                  fit: BoxFit.cover,
                ),
              ),
              Positioned(
                  top: 8,
                  right: 8,
                  child: Container(
                    padding:
                        const EdgeInsets.symmetric(horizontal: 4, vertical: 3),
                    decoration: BoxDecoration(
                      color: model.eventStatus == "Upcoming"
                          ? Colors.blue // Background color for "Upcoming"
                          : model.eventStatus == "Ongoing"
                              ? Colors.orange // Background color for "Ongoing"
                              : Colors.red, // Background color for "Expired"
                      borderRadius: BorderRadius.circular(12),
                    ),
                    child: Row(
                      children: [
                        Icon(
                          model.eventStatus == "Upcoming"
                              ? Icons.access_time // Icon for "Upcoming"
                              : model.eventStatus == "Ongoing"
                                  ? Icons.circle // Icon for "Ongoing"
                                  : Icons.timer_off, // Icon for "Expired"
                          color: Colors.white,
                        ),
                        SizedBox(width: 8), // Space between icon and text
                        Text(
                          model.eventStatus == "Upcoming"
                              ? "Upcoming"
                              : model.eventStatus == "Ongoing"
                                  ? "Ongoing"
                                  : "Expired", // Display event status text
                          style: TextStyle(
                            color: Colors.white, // Text color
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                      ],
                    ),
                  )),
            ],
          ),
          // Venue and Date section
          Padding(
            padding: const EdgeInsets.all(12.0),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                // Venue with map pin icon
                Row(
                  children: [
                    Icon(
                      Icons.location_on,
                      color: Colors.black, // You can change the color
                    ),
                    const SizedBox(width: 8),
                    Text(
                      model.venue,
                      style: Theme.of(context).textTheme.headline6,
                    ),
                  ],
                ),
                const SizedBox(height: 8),

                // Date with calendar icon
                Row(
                  children: [
                    Icon(
                      Icons.calendar_month_outlined,
                      color: Colors.blue, // You can change the color
                    ),
                    const SizedBox(width: 8),
                    Text(
                      '${model.startDate} - ${model.endDate}', // Directly use the strings
                      style: TextStyle(color: Colors.grey[600]),
                    ),
                  ],
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
