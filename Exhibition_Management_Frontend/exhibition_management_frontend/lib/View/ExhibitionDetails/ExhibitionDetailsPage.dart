import 'package:flutter/material.dart';

class ExhibitionDetailsPage extends StatelessWidget {
  final String bannerImageUrl;
  final List<String> venuePhotos;
  final String venueName;
  final String address;
  final String timeDuration;
  final int numberOfTables;
  final String description;

  const ExhibitionDetailsPage({
    Key? key,
    required this.bannerImageUrl,
    required this.venuePhotos,
    required this.venueName,
    required this.address,
    required this.timeDuration,
    required this.numberOfTables,
    required this.description,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(venueName),
        backgroundColor: Colors.white, // AppBar color
      ),
      body: SafeArea(
        child: Stack(
          children: [
            Container(
              color: Colors.grey[300], // Grey background for the page
              child: Padding(
                padding: const EdgeInsets.only(
                    bottom: 60.0), // Space for button at the bottom
                child: SingleChildScrollView(
                  child: Padding(
                    padding: const EdgeInsets.all(12.0),
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        // Banner Image
                        _buildSectionContainer(
                          title: "Banner",
                          icon: Icons.image,
                          child: GestureDetector(
                            onTap: () {
                              showDialog(
                                context: context,
                                builder: (context) => Dialog(
                                  child: GestureDetector(
                                    onTap: () =>
                                        Navigator.pop(context), // Close on tap
                                    child: Image.asset(
                                      bannerImageUrl,
                                      fit: BoxFit.contain,
                                    ),
                                  ),
                                ),
                              );
                            },
                            child: Image.asset(
                              bannerImageUrl,
                              height: 200,
                              width: double.infinity,
                              fit: BoxFit.cover,
                            ),
                          ),
                        ),

                        const SizedBox(height: 12),

                        // Venue Photos
                        _buildSectionContainer(
                          title: "Venue Photos",
                          icon: Icons.photo_album,
                          child: GestureDetector(
                            onTap: () {
                              showModalBottomSheet(
                                context: context,
                                isScrollControlled: true,
                                builder: (context) => Container(
                                  padding: const EdgeInsets.all(12.0),
                                  child: ListView(
                                    children: venuePhotos.map((url) {
                                      return Padding(
                                        padding:
                                            const EdgeInsets.only(bottom: 8.0),
                                        child: Image.asset(
                                          url,
                                          height: 300,
                                          width: double.infinity,
                                          fit: BoxFit.cover,
                                        ),
                                      );
                                    }).toList(),
                                  ),
                                ),
                              );
                            },
                            child: SingleChildScrollView(
                              scrollDirection: Axis.horizontal,
                              child: Row(
                                children: venuePhotos.map((url) {
                                  return Padding(
                                    padding: const EdgeInsets.only(right: 8.0),
                                    child: Image.asset(
                                      url,
                                      height: 100,
                                      width: 100,
                                      fit: BoxFit.cover,
                                    ),
                                  );
                                }).toList(),
                              ),
                            ),
                          ),
                        ),

                        const SizedBox(height: 12),

                        // Address
                        _buildSectionContainer(
                          title: "Address",
                          icon: Icons.location_on,
                          child: Text(
                            address,
                            style: const TextStyle(fontSize: 16),
                          ),
                        ),
                        const SizedBox(height: 12),

                        // Time Duration
                        _buildSectionContainer(
                          title: "Time Duration",
                          icon: Icons.schedule,
                          child: Text(
                            timeDuration,
                            style: const TextStyle(fontSize: 16),
                          ),
                        ),
                        const SizedBox(height: 12),

                        // Number of Tables
                        _buildSectionContainer(
                          title: "Number of Tables",
                          icon: Icons.table_chart,
                          child: Text(
                            numberOfTables.toString(),
                            style: const TextStyle(fontSize: 16),
                          ),
                        ),
                        const SizedBox(height: 12),

                        // Description
                        _buildSectionContainer(
                          title: "Description",
                          icon: Icons.description,
                          child: Text(
                            description,
                            style: const TextStyle(fontSize: 16),
                          ),
                        ),
                      ],
                    ),
                  ),
                ),
              ),
            ),
            Positioned(
              left: 0,
              right: 0,
              bottom: 0,
              child: Container(
                color: Colors.white,
                padding: const EdgeInsets.all(12.0),
                child: ElevatedButton(
                  onPressed: () {
                    print('Book Now clicked!');
                  },
                  style: ElevatedButton.styleFrom(
                    minimumSize: const Size(double.infinity, 50),
                    backgroundColor: Colors.green, // Button color
                  ),
                  child: const Text(
                    'Book Now',
                    style: TextStyle(fontSize: 18, color: Colors.white),
                  ),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildSectionContainer({
    required String title,
    required Widget child,
    required IconData icon,
  }) {
    return Container(
      padding: const EdgeInsets.all(12.0),
      decoration: BoxDecoration(
        color: Colors.white, // White section container color
        borderRadius: BorderRadius.circular(8.0),
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Row(
            children: [
              Icon(icon, size: 24, color: Colors.black), // Icon color
              const SizedBox(width: 8),
              Text(
                title,
                style: const TextStyle(
                  fontSize: 18,
                  fontWeight: FontWeight.bold,
                  color: Colors.black, // Text color
                ),
              ),
            ],
          ),
          const SizedBox(height: 8),
          child,
        ],
      ),
    );
  }
}
